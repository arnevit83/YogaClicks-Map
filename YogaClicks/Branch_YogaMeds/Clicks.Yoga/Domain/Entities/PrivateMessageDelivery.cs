using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class PrivateMessageDelivery : Entity
    {
        public virtual EntityHandle Recipient { get; set; }

        public virtual PrivateMessage Message { get; set; }

        public bool Read { get; set; }

        public bool Hidden { get; set; }

        public DateTime? ReadTime { get; set; }
    }
}