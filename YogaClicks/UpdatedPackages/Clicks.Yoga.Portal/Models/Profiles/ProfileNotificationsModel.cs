using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileNotificationsModel
    {
        public ProfileNotificationsModel(IEnumerable<NotificationDelivery> deliveries, IEnumerable<Request> requests)
        {
            Periods = new List<ProfileNotificationsPeriodModel>();
            Requests = new List<RequestModel>();

            ProfileNotificationsPeriodModel period = null;

            foreach (var delivery in deliveries)
            {
                if (period == null || delivery.CreationTime.Date != period.Date)
                {
                    period = new ProfileNotificationsPeriodModel(delivery.CreationTime.Date);
                    Periods.Add(period);
                }

                period.Deliveries.Add(new NotificationDeliveryModel(delivery));
            }

            foreach (var request in requests)
            {
                Requests.Add(new RequestModel(request));
            }
        }

        public IList<ProfileNotificationsPeriodModel> Periods { get; private set; }

        public IList<RequestModel> Requests { get; private set; }
    }
}