using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface INotificationService
    {
        IList<User> GetUsersDueDigest(); 
        int GetUnreadNotificationCount(User user);
        IList<NotificationDelivery> GetUnreadNotifications(User user);
        IList<NotificationDelivery> GetRecentNotifications(User user);
        IList<NotificationCategory> GetDigestNotificationCategories();
        void Dismiss(IEnumerable<NotificationDelivery> deliveries);
        void Notify(User user, string typeTag, IActor actor, IEntityHandle subject);
        void Notify(User user, string typeTag, IActor actor, IEntity entity);
        void Notify(User user, string typeTag, IActor actor, IEntityHandle subject, IEntityHandle context);
        void Notify(User user, string typeTag, IActor actor, IEntityHandle subject, IEntityHandle context, IEntity entity);
        IList<NotificationCategory> GetSelectedDigestNotificationCategories(User user);
        void ToggleNotificationCategoryEnabled(User user, int categoryId, bool enabled);
        void ToggleNotificationDigest(User user, bool enabled, string timescaleTag);
        void EnsureNotificationPreferencesExist(User user);
    }
}