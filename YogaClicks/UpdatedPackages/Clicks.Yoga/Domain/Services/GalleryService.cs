using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Permissions;

namespace Clicks.Yoga.Domain.Services
{
    public class GalleryService : ServiceBase, IGalleryService
    {
        public GalleryService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService,
            IImageService imageService,
            INewsService newsService,
            IWallService wallService,
            IGalleryPermissionProviderFactory permissionProviderFactory)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            FriendService = friendService;
            ImageService = imageService;
            NewsService = newsService;
            WallService = wallService;
            PermissionProviderFactory = permissionProviderFactory;
            }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private IImageService ImageService { get; set; }

        private INewsService NewsService { get; set; }

        private IWallService WallService { get; set; }

        private IGalleryPermissionProviderFactory PermissionProviderFactory { get; set; }

        public bool GalleryVisible(Gallery gallery)
        {
            if (gallery.Public && gallery.Owner.Profile.Public) return true;

            if (!SecurityContext.Authenticated) return false;

            if (SecurityContext.CanUpdate(gallery)) return true;

            var status = FriendService.GetFriendshipStatus(gallery.Owner.Profile.Id, SecurityContext.CurrentUser.Profile.Id);

            return status == FriendshipStatus.Friends;
        }

        public bool EntryPromoted(GalleryEntry entry)
        {
            return entry.Promoted;
        }

        public IList<Gallery> GetAssociatedGalleriesForDisplay(int entityId, string entityTypeName)
        {
            var galleries = EntityContext.Galleries
                .Include(e => e.Cover)
                .Include(e => e.Owner)
                .Include(e => e.Owner.Profile)
                .Where(e => e.Associations.Any(a =>
                    a.Handle.EntityId == entityId &&
                    a.Handle.EntityType.Name == entityTypeName &&
                    a.Handle.Active))
                .ToList();

            foreach (var gallery in galleries.ToList())
            {
                if (!GalleryVisible(gallery)) galleries.Remove(gallery);
            }

            return galleries;
        }

        public IList<GalleryEntry> GetEntries(int galleryId)
        {
            return EntityContext.GalleryEntries
                .Include(e => e.Image)
                .Include(e => e.Image.Type)
                .Where(e => e.Gallery.Id == galleryId)
                .Where(e => e.Gallery.Active)
                .OrderBy(e => e.CreationTime)
                .ToList();
        }

        public Gallery GetNewsfeedGallery(int ownerId)
        {
            return EntityContext.Galleries.Get(e => e.Owner.Id == ownerId && e.IsNewsfeed);
        }

        public Gallery GetGallery(int id)
        {
            return EntityContext.Galleries.Get(id);
        }

        public Gallery GetGalleryForDisplay(int id)
        {
            var gallery = GetGallery(id);

            if (gallery == null)
                throw new UnknownEntityException();
            if (!GalleryVisible(gallery))
                throw new PermissionDeniedException();

            return gallery;
        }

        public Gallery GetGalleryForEditing(int id)
        {
            var gallery = GetGalleryForDisplay(id);

            if (!SecurityContext.CanUpdate(gallery))
                throw new PermissionDeniedException();

            return gallery;
        }

        public GalleryEntry GetEntryForDisplay(int id)
        {
            var entry = EntityContext.GalleryEntries.Get(id);

            if (entry == null)
                throw new UnknownEntityException();
            if (!GalleryVisible(entry.Gallery))
                throw new PermissionDeniedException();

            return entry;
        }

        public GalleryEntry GetEntryForEditing(int id)
        {
            var entry = GetEntryForDisplay(id);

            if (!SecurityContext.CanUpdate(entry.Gallery))
                throw new PermissionDeniedException();

            return entry;
        }

        public GalleryPermissions GetPermissions(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IGalleryAssociate>(entityId, entityTypeName);

            if (entity == null)
                throw new ArgumentException("Entity does not exist or does not implement IGalleryAssociate.");

            var provider = PermissionProviderFactory.GetProvider(entity);

            if (provider == null)
                throw new Exception("Failed to obtain gallery permission provider.");

            return provider.GetPermissions(entity);
        }

        public Gallery CreateGallery(int ownerId)
        {
            var owner = EntityContext.Users.Get(ownerId);

            if (owner == null)
                throw new ArgumentOutOfRangeException("ownerId");
            if (!SecurityContext.CanUpdate(owner))
                throw new PermissionDeniedException();

            var gallery = new Gallery { Owner = owner };

            var association = new GalleryAssociation
            {
                Gallery = gallery,
                Handle = EntityService.EnsureEntityHandle(owner.Profile)
            };

            gallery.Associations.Add(association);

            EntityContext.Galleries.Add(gallery);

            return gallery;
        }

        public Gallery CreateNewsfeedGallery(int ownerId)
        {
            var gallery = CreateGallery(ownerId);

            gallery.Name = "Newsfeed Photos";
            gallery.IsNewsfeed = true;

            return gallery;
        }

        public GalleryEntry CreateEntry(Gallery gallery, int imageId, string name)
        {
            if (gallery == null)
                throw new ArgumentNullException("gallery");
            if (name == null)
                throw new ArgumentNullException("name");

            var image = EntityContext.Images.Get(imageId);

            if (image == null)
                throw new ArgumentOutOfRangeException("imageId");

            image.Temporary = false;

            return CreateEntry(gallery, image, name);
        }

        public GalleryEntry CreateEntry(Gallery gallery, Stream stream, string contentType, string name)
        {
            if (gallery == null)
                throw new ArgumentNullException("gallery");
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");

            var image = ImageService.CreateImage(typeof(GalleryEntry).GetProperty("Image"), stream, contentType);

            return CreateEntry(gallery, image, name);
        }

        public void AssociateGallery(Gallery gallery, int handleId)
        {
            if (gallery == null)
                throw new ArgumentNullException("gallery");

            if (gallery.Associations.Any(a => a.Handle.Id == handleId)) return;

            var handle = EntityContext.EntityHandles.Get(handleId);

            if (handle == null)
                throw new ArgumentOutOfRangeException("handleId");

            var permissions = GetPermissions(handle.EntityId, handle.EntityType.Name);

            if (!permissions.Associate)
                throw new PermissionDeniedException();

            gallery.Associations.Add(new GalleryAssociation { Gallery = gallery, Handle = handle });
        }

        public void DisassociateGallery(Gallery gallery, int handleId)
        {
            if (gallery == null)
                throw new ArgumentNullException("gallery");

            var association = gallery.Associations.FirstOrDefault(a => a.Handle.Id == handleId);

            if (association == null) return;

            var permissions = GetPermissions(association.Handle.EntityId, association.Handle.EntityType.Name);

            if (!permissions.Disassociate && !SecurityContext.CanUpdate(gallery))
                throw new PermissionDeniedException();

            gallery.Associations.Remove(association);

            EntityContext.GalleryAssociations.Remove(association);

            if (gallery.Associations.Count == 0 && SecurityContext.CanUpdate(gallery))
            {
                DeleteGallery(gallery.Id);
            }
        }

        public void PromoteEntry(GalleryEntry entry)
        {
            entry.Promoted = true;
        }

        public void UnpromoteEntry(GalleryEntry entry)
        {
            entry.Promoted = false;
        }

        public void DeleteGallery(int id)
        {
            var gallery = EntityContext.Galleries.Get(id);

            if (gallery == null)
                throw new ArgumentOutOfRangeException("id");
            if (!SecurityContext.CanDelete(gallery))
                throw new PermissionDeniedException();
            
            foreach (var entry in gallery.Entries.ToList())
            {
                DeleteEntry(entry.Id);
            }

            EntityContext.Galleries.Remove(gallery);
        }

        public void DeleteEntry(int id)
        {
            var entry = EntityContext.GalleryEntries.Get(id);

            if (entry == null)
                throw new ArgumentOutOfRangeException("id");
            if (!SecurityContext.CanUpdate(entry.Gallery) && !SecurityContext.CanUpdate(entry.Image))
                throw new PermissionDeniedException();

            var resources = EntityContext.MediaResources
                .Where(r => r.EntityId == entry.Id)
                .Where(r => r.Type.EntityType.Name == typeof(GalleryEntry).Name)
                .ToList();

            foreach (var resource in resources)
            {
                WallService.DeleteResource(resource);
            }

            entry.Image.Active = false;

            EntityContext.GalleryEntries.Remove(entry);
        }

        private GalleryEntry CreateEntry(Gallery gallery, Image image, string name)
        {
            if (gallery == null)
                throw new ArgumentNullException("gallery");
            if (image == null)
                throw new ArgumentNullException("image");
            if (name == null)
                throw new ArgumentNullException("name");

            if (!gallery.Open && !SecurityContext.CanUpdate(gallery))
                throw new PermissionDeniedException();
            if (!SecurityContext.CanUpdate(image))
                throw new PermissionDeniedException();

            var entry = new GalleryEntry
            {
                Gallery = gallery,
                Image = image,
                Name = name
            };

            EntityContext.GalleryEntries.Add(entry);
            gallery.Entries.Add(entry);

            if (gallery.Cover == null) gallery.Cover = image;

            if (!gallery.IsNewsfeed)
            {
                NewsService.PostAction(NewsStoryType.FriendAddedPhotos, SecurityContext.CurrentProfile, gallery);
            }

            return entry;
        }
    }
}