using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class NotificationCategoryModel : EntityModel<NotificationCategory>
    {
        public NotificationCategoryModel() {}

        public NotificationCategoryModel(NotificationCategory entity) : base(entity) {}

        public string Name { get; set; }

        public override void Populate(NotificationCategory entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}