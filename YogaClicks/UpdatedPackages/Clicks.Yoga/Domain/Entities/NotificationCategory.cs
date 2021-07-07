using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class NotificationCategory : Entity
    {
        public NotificationCategory()
        {
            NotificationTypes = new List<NotificationType>();
        }

        public string Tag { get; set; }

        public string Name { get; set; }

        public bool IncludedInDigest { get; set; }

        public bool IsFriendshipRequests { get; set; }

        public virtual ICollection<NotificationType> NotificationTypes { get; private set; }

        public virtual ICollection<RequestType> RequestTypes { get; private set; } 
    }
}