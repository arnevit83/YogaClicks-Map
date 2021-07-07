using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class PrivateMessage : Entity
    {
        public PrivateMessage()
        {
            Deliveries = new List<PrivateMessageDelivery>();
        }

        public virtual PrivateConversation Conversation { get; set; }

        public virtual EntityHandle Sender { get; set; }

        public string Content { get; set; }

        public virtual IList<PrivateMessageDelivery> Deliveries { get; private set; } 
    }
}