using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Permissions;

namespace Clicks.Yoga.Domain.Services
{
    public class VideoService : ServiceBase, IVideoService
    {
        public VideoService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IGalleryPermissionProviderFactory permissionProviderFactory)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            PermissionProviderFactory = permissionProviderFactory;
        }

        private IEntityService EntityService { get; set; }

        private IGalleryPermissionProviderFactory PermissionProviderFactory { get; set; }

        public IList<AbilityLevel> GetAbilityLevels()
        {
            return EntityContext.AbilityLevels.Where(l => !l.IsOpen).OrderBy(l => l.Index).ToList();
        }

        public IList<Video> GetAssociatedVideos(int entityId, string entityTypeName)
        {
            return EntityContext.Videos
                .Where(e => e.Associations.Any(a =>
                    a.Handle.EntityId == entityId &&
                    a.Handle.EntityType.Name == entityTypeName &&
                    a.Handle.Active))
                .ToList();
        }

        public AbilityLevel GetAbilityLevel(int id)
        {
            return EntityContext.AbilityLevels.Get(id);
        }

        public Video GetVideo(int id)
        {
            return EntityContext.Videos.Get(id);
        }

        public Video CreateVideo(string url)
        {
             if (url == null)
                 throw new ArgumentNullException("url");

            VideoType type;
            string identifier;

            if (TryParseUrl(url, out type, out identifier))
            {
                var video = new Video
                {
                    Owner = SecurityContext.CurrentUser,
                    Type = type,
                    Identifier = identifier
                };

                EntityContext.Videos.Add(video);

                return video;
            }
            else
            {
               throw new ArgumentException("Unknown URL format.", "url");
            }

        }

        public Video CreateVideo(string url, int entityId, string entityTypeName)
        {
            var video = CreateVideo(url);
            
                var association = new VideoAssociation
                {
                    Video = video,
                    Handle = EntityService.EnsureEntityHandle(entityId, entityTypeName)
                };

                video.Associations.Add(association);

                return video;
        }

        public void AssociateVideo(Video video, int handleId)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            if (video.Associations.Any(a => a.Handle.Id == handleId)) return;

            var handle = EntityContext.EntityHandles.Get(handleId);

            if (handle == null)
                throw new ArgumentOutOfRangeException("handleId");

            var permissions = GetPermissions(handle.EntityId, handle.EntityType.Name);

            if (!permissions.Associate)
                throw new PermissionDeniedException();

            video.Associations.Add(new VideoAssociation { Video = video, Handle = handle });
        }

        public void DisassociateVideo(Video video, int handleId)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            var association = video.Associations.FirstOrDefault(a => a.Handle.Id == handleId);

            if (association == null) return;

            var permissions = GetPermissions(association.Handle.EntityId, association.Handle.EntityType.Name);

            if (!permissions.Disassociate && !SecurityContext.CanUpdate(video))
                throw new PermissionDeniedException();

            video.Associations.Remove(association);

            EntityContext.VideoAssociations.Remove(association);

            if (video.Associations.Count == 0 && SecurityContext.CanUpdate(video))
            {
                DeleteVideo(video.Id);
            }
        }

        public void DeleteVideo(int id)
        {
            var video = EntityContext.Videos.Get(id);

            if (video == null)
                throw new ArgumentOutOfRangeException("id");
            if (!SecurityContext.CanDelete(video))
                throw new PermissionDeniedException();

            EntityContext.Videos.Remove(video);
        }

        private bool TryParseUrl(string url, out VideoType type, out string identifier)
        {
            var types = EntityContext.VideoTypes.ToList();

            foreach (var t in types)
            {
                if (t.TryParseUrl(url, out identifier))
                {
                    type = t;
                    return true;
                }
            }

            type = null;
            identifier = null;

            return false;
        }

        private GalleryPermissions GetPermissions(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IGalleryAssociate>(entityId, entityTypeName);

            if (entity == null)
                throw new ArgumentException("Entity does not exist or does not implement IGalleryAssociate.");

            var provider = PermissionProviderFactory.GetProvider(entity);

            if (provider == null)
                throw new Exception("Failed to obtain gallery permission provider.");

            return provider.GetPermissions(entity);
        }
    }
}