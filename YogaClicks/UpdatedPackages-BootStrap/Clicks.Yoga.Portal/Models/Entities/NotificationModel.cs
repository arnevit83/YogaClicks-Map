using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class NotificationModel : EntityModel<Notification>
    {
        public NotificationModel(Notification entity) : base(entity) {}

        public NotificationTypeModel Type { get; set; }

        public string Message { get; set; }

        public override void Populate(Notification entity)
        {
            Type = new NotificationTypeModel(entity.Type);
            Message = entity.GetMessage();
        }
    }
}