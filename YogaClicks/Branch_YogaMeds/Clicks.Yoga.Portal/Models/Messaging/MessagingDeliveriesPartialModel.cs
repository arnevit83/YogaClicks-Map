using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingDeliveriesPartialModel
    {
        public MessagingDeliveriesPartialModel(IEnumerable<PrivateMessageDelivery> deliveries)
        {
            Deliveries = new List<PrivateMessageDeliveryModel>();

            foreach (var delivery in deliveries)
            {
                Deliveries.Add(new PrivateMessageDeliveryModel(delivery));
            }
        }

        public IList<PrivateMessageDeliveryModel> Deliveries { get; private set; }
    }
}