using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class NotificationDeliveryModel : EntityModel<NotificationDelivery>
    {
        public NotificationDeliveryModel(NotificationDelivery entity) : base(entity) {}

        public override void Populate(NotificationDelivery entity)
        {
            Notification = new NotificationModel(entity.Notification);
            Read = entity.Read;
        }

        public NotificationModel Notification { get; set; }

        public bool Read { get; set; }
    }
}