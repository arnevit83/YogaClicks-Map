using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class NotificationService : ServiceBase, INotificationService
    {
        public NotificationService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
        }

        private IEntityService EntityService { get; set; }

        public IList<User> GetUsersDueDigest()
        {
            return EntityContext.Users
                .Where(e => e.Active)
                .Where(e => e.NotificationPreferences.EmailDigestEnabled)
                .Where(e => e.NotificationPreferences.NextDigestEmailTime < DateTime.Now)
                .ToList();
        }

        public int GetUnreadNotificationCount(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (!SecurityContext.CanUpdate(user))
                throw new PermissionDeniedException();

            return EntityContext.Notifications
                .Where(n =>
                    (n.Broadcast && !n.Deliveries.Any(d => d.User.Id == user.Id && d.Read)) ||
                    (!n.Broadcast && n.Deliveries.Any(d => d.User.Id == user.Id && !d.Read)))
                .Where(n => n.ActorHandle == null || n.ActorHandle.Active)
                .Where(n => n.SubjectHandle == null || n.SubjectHandle.Active)
                .Count();
        }

        public IList<NotificationDelivery> GetUnreadNotifications(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (!SecurityContext.CanUpdate(user))
                throw new PermissionDeniedException();

            return GetDeliveries(user, d => !d.Read);
        }

        public IList<NotificationDelivery> GetRecentNotifications(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (!SecurityContext.CanUpdate(user))
                throw new PermissionDeniedException();

            return GetDeliveries(user, d => true);
        }

        public IList<NotificationCategory> GetDigestNotificationCategories()
        {
            return EntityContext.NotificationCategories.Where(e => e.IncludedInDigest).OrderBy(e => e.Name).ToList();
        }

        public void Dismiss(IEnumerable<NotificationDelivery> deliveries)
        {
            foreach (var delivery in deliveries)
            {
                delivery.Read = true;
            }
        }

        public void Notify(User user, string typeTag, IActor actor, IEntityHandle subject)
        {
            Notify(user, typeTag, actor, subject, null);
        }

        public void Notify(User user, string typeTag, IActor actor, IEntity entity)
        {
            Notify(user, typeTag, actor, null, null, entity);
        }

        public void Notify(User user, string typeTag, IActor actor, IEntityHandle subject, IEntityHandle context)
        {
            Notify(user, typeTag, actor, subject, context, null);
        }

        public void Notify(User user, string typeTag, IActor actor, IEntityHandle subject, IEntityHandle context, IEntity entity)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (typeTag == null)
                throw new ArgumentNullException("typeTag");

            var type = EntityContext.NotificationTypes.Get(t => t.Tag == typeTag);

            if (type == null)
                throw new ArgumentOutOfRangeException("typeTag");

            if (
                type.Category != null &&
                EntityContext.NotificationCategoryPreferences.Any(e =>
                    e.Category.Id == type.Category.Id &&
                    e.User.Id == user.Id &&
                    !e.NotificationEnabled))
            {
                return;
            }

            EntityHandle actorHandle = null;
            EntityHandle subjectHandle = null;
            EntityHandle contextHandle = null;

            if (actor != null) actorHandle = EntityService.EnsureEntityHandle(actor);
            if (subject != null) subjectHandle = EntityService.EnsureEntityHandle(subject);
            if (context != null) contextHandle = EntityService.EnsureEntityHandle(context);

            int? entityId = null;
            EntityType entityType = null;

            if (entity != null)
            {
                entityId = entity.Id;
                entityType = EntityService.GetEntityType(entity.GetEntityTypeName());
            }

            var notification = new Notification
            {
                Type = type,
                ActorHandle = actorHandle,
                ContextHandle = contextHandle,
                SubjectHandle = subjectHandle,
                EntityId = entityId,
                EntityType = entityType
            };

            var delivery = new NotificationDelivery
            {
                User = user,
                Notification = notification
            };

            EntityContext.NotificationDeliveries.Add(delivery);
        }

        public IList<NotificationCategory> GetSelectedDigestNotificationCategories(User user)
        {
            var categories = EntityContext.NotificationCategories.Where(c => c.IncludedInDigest).ToList();
            var preferences = EntityContext.NotificationCategoryPreferences.Where(e => e.User.Id == user.Id).ToList();

            categories.RemoveAll(c => preferences.Any(p => p.Category.Id == c.Id && !p.NotificationEnabled));

            return categories;
        }

        public void ToggleNotificationCategoryEnabled(User user, int categoryId, bool enabled)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var cateogry = EntityContext.NotificationCategories.Get(categoryId);

            if (cateogry == null)
                throw new ArgumentOutOfRangeException("categoryId");

            var preferences = EntityContext.NotificationCategoryPreferences.Get(e => e.User.Id == user.Id && e.Category.Id == categoryId);

            if (preferences == null)
            {
                if (enabled) return;

                preferences = new NotificationCategoryPreferences { User = user, Category = cateogry };
                EntityContext.NotificationCategoryPreferences.Add(preferences);
            }
            else
            {
                preferences.NotificationEnabled = enabled;
            }
        }

        public void ToggleNotificationDigest(User user, bool enabled, string timescaleTag)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var preferences = EntityContext.NotificationPreferences.Get(p => p.User.Id == user.Id);

            if (preferences == null)
            {
                preferences = new NotificationPreferences { User = user };
                EntityContext.NotificationPreferences.Add(preferences);
            }

            preferences.EmailDigestEnabled = enabled;
            
            if (enabled)
            {
                var timescale = EntityContext.Timescales.Get(e => e.Tag == timescaleTag);

                if (timescale == null)
                    throw new ArgumentOutOfRangeException("timescaleTag");

                preferences.EmailDigestTimescale = timescale;
                preferences.NextDigestEmailTime = DateTime.Now;
                preferences.CalculateNextDigestEmailTime();
            }
            else if (preferences.EmailDigestTimescale == null)
            {
                preferences.EmailDigestTimescale = EntityContext.Timescales.First();
            }
        }

        public void EnsureNotificationPreferencesExist(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (user.NotificationPreferences == null)
            {
                ToggleNotificationDigest(user, true, "Week");
            }
        }

        private IList<NotificationDelivery> GetDeliveries(User user, Expression<Func<NotificationDelivery, bool>> predicate)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var deliveries = EntityContext.NotificationDeliveries
                .Where(d => d.User.Id == user.Id)
                .Where(d => d.Notification.ActorHandle == null || d.Notification.ActorHandle.Active)
                .Where(d => d.Notification.SubjectHandle == null || d.Notification.SubjectHandle.Active)
                .Where(predicate)
                .ToList();

            var broadcasts = EntityContext.Notifications
                .Where(n => n.Broadcast)
                .Where(n => !n.Deliveries.Any(d => d.User.Id == user.Id))
                .ToList();

            foreach (var notification in broadcasts)
            {
                var delivery = new NotificationDelivery { Notification = notification, User = user };
                EntityContext.NotificationDeliveries.Add(delivery);
                deliveries.Add(delivery);
            }

            return deliveries.Where(predicate.Compile()).OrderByDescending(d => d.Notification.CreationTime).ToList();
        }
    }
}