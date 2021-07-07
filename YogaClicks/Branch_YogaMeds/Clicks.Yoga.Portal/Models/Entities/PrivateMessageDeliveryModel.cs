using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class PrivateMessageDeliveryModel : EntityModel<PrivateMessageDelivery>
    {
        public PrivateMessageDeliveryModel(PrivateMessageDelivery entity) : base(entity) {}

        public EntityHandleModel Recipient { get; private set; }

        public PrivateMessageModel Message { get; private set; }

        public bool Read { get; private set; }

        public override void Populate(PrivateMessageDelivery entity)
        {
            Recipient = new EntityHandleModel(entity.Recipient);
            Message = new PrivateMessageModel(entity.Message);
            Read = entity.Read;
        }
    }
}