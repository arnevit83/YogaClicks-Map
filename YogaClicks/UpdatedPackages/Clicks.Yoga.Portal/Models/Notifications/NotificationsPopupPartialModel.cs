using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Notifications
{
    public class NotificationsPopupPartialModel
    {
        public NotificationsPopupPartialModel(IEnumerable<NotificationDelivery> deliveries, IEnumerable<Request> requests)
        {
            Deliveries = new List<NotificationDeliveryModel>();
            Requests = new List<RequestModel>();

            foreach (var delivery in deliveries)
            {
                Deliveries.Add(new NotificationDeliveryModel(delivery));
            }

            foreach (var request in requests)
            {
                Requests.Add(new RequestModel(request));
            }
        }

        public IList<NotificationDeliveryModel> Deliveries { get; private set; }

        public IList<RequestModel> Requests { get; private set; }
    }
}