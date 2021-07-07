using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class NotificationTypeModel : EntityModel<NotificationType>
    {
        public NotificationTypeModel(NotificationType entity) : base(entity) {}

        public string Tag { get; set; }

        public string Icon { get; set; }

        public string IconUri
        {
            get { return string.Format("/Images/{0}", Icon); }
        }

        public override void Populate(NotificationType entity)
        {
            Tag = entity.Tag;
            Icon = entity.Icon;
        }
    }
}