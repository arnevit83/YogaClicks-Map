using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingPopupPartialModel
    {
        public MessagingPopupPartialModel(IEnumerable<PrivateMessageDelivery> deliveries)
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