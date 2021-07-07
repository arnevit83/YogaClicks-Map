using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class ProfileService : ServiceBase, IProfileService
    {
        private static readonly List<string> ProfessionalEntityTypes = new List<string> {
            "Teacher",
            "Venue",
            "TeacherTrainingOrganisation",
            "StyleOrganisation"
        }; 

        public ProfileService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            ISearchService searchService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            SearchService = searchService;
        }

        private IEntityService EntityService { get; set; }

        private ISearchService SearchService { get; set; }

        public Gender GetGender(int id)
        {
            return EntityContext.Genders.Get(id);
        }

        public Profile GetProfile(int id)
        {
            return EntityContext.Profiles.GetIgnoringActive(id);
        }

        public Profile GetProfileForDisplay(int id)
        {
            var profile = GetProfile(id);
            if (profile == null || !profile.Active) throw new UnknownEntityException();
            //if (!ProfileVisibleToCurrentUser(id)) throw new PermissionDeniedException();
            return profile;
        }

        public Profile GetProfileForEditing(int id)
        {
            var profile = GetProfileForDisplay(id);
            if (!SecurityContext.CanUpdate(profile)) throw new PermissionDeniedException();
            return profile;
        }

        public Profile GetProfile(PrincipalEntity entity)
        {
            if (entity.Owner == null) return null;
            return EntityContext.Profiles.GetIgnoringActive(e => e.Owner.Id == entity.Owner.Id);
        }

        public Profile GetProfile(ISecurable securable)
        {
            if (securable == null || securable.OwnerId == null) return null;
            return EntityContext.Profiles.GetIgnoringActive(e => e.Owner.Id == securable.OwnerId.Value);
        }

        public bool ProfileVisibleToCurrentUser(int profileId)
        {
            if (!SecurityContext.Authenticated) return false;
            if (SecurityContext.CurrentUser.IsSuperUser) return true;

            var profile = EntityContext.Profiles.GetIgnoringActive(profileId);

            if (profile == null)
                throw new ArgumentOutOfRangeException("profileId");

            if (profile.Public || SecurityContext.IsOwner(profile)) return true;

            var friendship = EntityContext.Friendships.Get(f => f.Profile.Id == profile.Owner.Id && f.Friend.Owner.Id == SecurityContext.CurrentUser.Id);

            if (friendship == null || friendship.BlockedByProfile) return false;

            return true;
        }

        public IList<EntityType> GetProfessionalEntityTypes()
        {
            return EntityContext.EntityTypes
                .Where(e => e.IsProfessional)
                .ToList()
                .OrderBy(e => ProfessionalEntityTypes.IndexOf(e.Name))
                .ToList();
        }

        public IList<EntityHandle> GetProfessionalEntities()
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var user = SecurityContext.CurrentUser;

            return EntityContext.EntityHandles
                .Include(h => h.EntityType)
                .Where(h => h.Owner.Id == user.Id)
                .Where(h => h.EntityType.IsProfessional)
                .Where(h => h.Active)
                .OrderBy(h => h.EntityType.ActorOrdinal)
                .ThenBy(h => h.Name)
                .ToList();
        }

        public IList<EntityHandle> GetProfessionalEntitiesAndProfile()
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var user = SecurityContext.CurrentUser;
            var handles = GetProfessionalEntities();

            if (!user.Profile.Professional)
            {
                handles.Insert(0, EntityService.EnsureEntityHandle(user.Profile));
            }

            return handles;
        }

        public IList<EntityHandle> GetActors()
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            return EntityContext.EntityHandles
                .Where(h => h.Owner.Id == SecurityContext.CurrentUser.Id)
                .Where(h => h.EntityType.IsActor)
                .OrderBy(h => !h.EntityType.IsProfessional)
                .ThenBy(h => h.Name)
                .ToList();
        }

        public IList<EntityHandle> GetGalleryAssociateHandles()
        {
            return GetProfessionalEntities()
                .Where(e => e.EntityType.IsGalleryAssociate && e.EntityType.Name != "Profile")
                .ToList();
        }

        public IList<Group> GetGroups(int userId)
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            return EntityContext.Groups
                .Where(e => e.Owner.Id == userId)
                .OrderBy(e => e.Name)
                .ToList();
        }

        public IList<Image> GetPromotedGalleryImages(int userId)
        {
            return EntityContext.GalleryEntries
                .Include(e => e.Image)
                .Include(e => e.Image.Type)
                .Where(e => e.Gallery.Owner.Id == userId)
                .Where(e => e.Promoted)
                .Where(e => e.Gallery.Active)
                .OrderByDescending(e => e.ModificationTime)
                .Select(e => e.Image)
                .Take(10)
                .ToList();
        }

        public IList<Profile> GetFans()
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            return EntityContext.Fans
                .Include(f => f.User)
                .Include(f => f.User.Profile)
                .Where(f => f.EntityHandle.Owner.Id == SecurityContext.CurrentUser.Id)
                .Select(f => f.User.Profile)
                .ToList();
        }

        public IList<ProfileQuestion> GetProfileQuestions()
        {
            return EntityContext.ProfileQuestions.OrderBy(q => q.DisplayOrder).ToList();
        }

        public IList<ProfileAnswer> GetProfileAnswersForCurrentUser()
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var profile = SecurityContext.CurrentUser.Profile;
            return EntityContext.ProfileAnswers.Where(a => a.ProfileId == profile.Id).ToList();
        }

        public void AddProfile(Profile profile)
        {
            EntityContext.Profiles.Add(profile);
        }

        public void DeleteOwned(int id, string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            var entity = EntityService.GetEntity<PrincipalEntity>(id, typeName);

            if (entity == null)
                throw new UnknownEntityException();
            if (!SecurityContext.CanDelete(entity))
                throw new PermissionDeniedException();

            entity.Active = false;

            var search = entity as ISearchable;

            if (search != null)
            {
                SearchService.Index(search);
            }
        }

        public void UpdateProfileQuestions(Profile profile, IList<ProfileAnswer> answers)
        {
            var list = profile.ProfileAnswers.ToList();

            for (var ctr = list.Count - 1; ctr >= 0; ctr--)
                EntityContext.ProfileAnswers.Remove(list[ctr]);

            foreach (var answer in answers)
            {
                if (answer.ProfileId != profile.Id)
                    continue;

                profile.ProfileAnswers.Add(answer);
            }
        }

        public void Claim(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IClaimable>(entityId, entityTypeName);

            if (entity == null)
                throw new ArgumentException("Entity does not exist or does not implement IClaimable.");
            if (entity.Owner != null)
                throw new InvalidOperationException("Entity already has an owner.");

            entity.Owner = SecurityContext.CurrentUser;

            if (entity.Stubable) entity.Stubbed = false;
        }

        public void Unclaim(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IClaimable>(entityId, entityTypeName);

            if (entity == null)
                throw new ArgumentException("Entity does not exist or does not implement IClaimable.");
            if (entity.Owner == null)
                throw new InvalidOperationException("Entity does not have an owner.");

            entity.Owner = null;
            entity.Stubbed = true;

            DisassociateGalleries(entityId, entityTypeName);
        }

        public void Hide(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            profile.Active = false;
        }

        private void DisassociateGalleries(int entityId, string entityTypeName)
        {
            var entityType = EntityContext.EntityTypes.Get(e => e.Name == entityTypeName);
            var galleries = EntityContext.GalleryAssociations.Where(e => e.Handle.EntityId == entityId && e.Handle.EntityType.Id == entityType.Id).ToList();
            var i = galleries.Count - 1;

            while (i >= 0)
            {
                var association = galleries[i];
                var gallery = association.Gallery;
                var profileType = SecurityContext.CurrentProfile.GetEntityTypeName();
                var handle = EntityContext.EntityHandles.Get(e => e.EntityId == SecurityContext.CurrentProfile.Id && e.EntityType.Name == profileType);

                EntityContext.GalleryAssociations.Remove(association);
                gallery.Associations.Remove(association);

                if (!gallery.Associations.Any())
                {
                    gallery.Associations.Add(new GalleryAssociation { Gallery = gallery, Handle = handle });
                }

                galleries.RemoveAt(i);

                i--;
            }
        }
    }
}