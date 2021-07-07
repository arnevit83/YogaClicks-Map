using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class NotificationDigestEmail : EmailBase
    {
        public Profile Profile { get; set; }

        public int MessageCount { get; set; }

        public Dictionary<NotificationCategory, int> Notifications { get; set; }

        public Dictionary<NotificationCategory, int> Requests { get; set; }

        public bool AnyNotifications
        {
            get
            {
                return
                    MessageCount > 0 ||
                    Notifications.Values.Any(c => c > 0) ||
                    Requests.Values.Any(c => c > 0);
            }
        }

        public override string To
        {
            get { return Profile.Owner.EmailAddress; }
        }
    }
}
