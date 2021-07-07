using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class FanService : ServiceBase, IFanService
    {
        public FanService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            INewsService newsService,
            INotificationService notificationService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            NewsService = newsService;
            NotificationService = notificationService;
        }

        private IEntityService EntityService { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        public bool CanFan(User user, int entityId, string entityTypeName)
        {
            if (user == null) return false;
            if (user.Profile == null) return false;
            if (user.Profile.Professional) return false;

            var entity = EntityService.GetEntity<IEntityHandle>(entityId, entityTypeName);
            var securable = entity as ISecurable;

            if (entity == null) return false;
            if (securable != null && securable.OwnerId.HasValue && securable.OwnerId.Value == user.Id) return false;

            return true;
        }

        public bool IsFanned(User user, int entityId, string entityTypeName)
        {
            if (user == null) return false;

            return EntityContext.Fans.Any(f =>
                f.User.Id == user.Id &&
                f.EntityHandle.EntityId == entityId &&
                f.EntityHandle.EntityType.Name == entityTypeName);
        }

        public IList<EntityType> GetFanableTypes()
        {
            return EntityContext.EntityTypes.Where(e => e.IsFanable).ToList();
        }

        public IList<EntityHandle> GetFanned(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.EntityHandles.Where(h => h.Fans.Any(f => f.User.Id == user.Id)).ToList();
        }

        public IList<EntityHandle> GetFanned(User user, string entityTypeName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.EntityHandles.Where(h => h.Fans.Any(f => f.User.Id == user.Id && f.EntityHandle.EntityType.Name == entityTypeName)).ToList();
        }

        public IList<Profile> GetFans(int entityId, string entityTypeName)
        {
            if (entityTypeName == null)
                throw new ArgumentNullException("entityTypeName");

            return EntityContext.Fans
                .Include(f => f.User.Profile.Image)
                .Include(f => f.User.Profile.Location)
                .Where(f => f.EntityHandle.EntityId == entityId)
                .Where(f => f.EntityHandle.EntityType.Name == entityTypeName)
                .Where(f => f.EntityHandle.Active)
                .Select(f => f.User.Profile)
                .ToList();
        }

        public int GetFanCount(int entityId, string entityTypeName)
        {
            if (entityTypeName == null)
                throw new ArgumentNullException("entityTypeName");

            return EntityContext.Fans
                .Where(f => f.EntityHandle.EntityId == entityId)
                .Where(f => f.EntityHandle.EntityType.Name == entityTypeName)
                .Count();
        }

        public void Fan(User user, int entityId, string entityTypeName)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var entity = EntityService.GetEntity<IEntityHandle>(entityId, entityTypeName);

            if (entity == null)
                throw new ArgumentException("Entity does not exist or does not implement IEntityHandle.");

            var handle = EntityService.EnsureEntityHandle(entity);
            var fan = new Fan { User = user, EntityHandle = handle };

            EntityContext.Fans.Add(fan);

            var principal = entity as PrincipalEntity;

            if (principal != null && principal.Owner != null)
            {
                NotificationService.Notify(principal.Owner, "Fanned", SecurityContext.CurrentActor, entity);
            }

            if (entity.GetEntityTypeName() != "NewsStory")
            {
                NewsService.PostAction(NewsStoryType.FriendFanned, user.Profile, entity);
            }
        }

        public void Unfan(User user, int entityId, string entityTypeName)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (!SecurityContext.CanUpdate(user))
                throw new PermissionDeniedException();

            var fan = EntityContext.Fans.FirstOrDefault(f =>
                f.User.Id == user.Id &&
                f.EntityHandle.EntityId == entityId &&
                f.EntityHandle.EntityType.Name == entityTypeName);

            if (fan == null) return;

            EntityContext.Fans.Remove(fan);
        }
    }
}