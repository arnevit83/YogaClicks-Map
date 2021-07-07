using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityTypeModel : EntityModel<ActivityType>
    {
        public ActivityTypeModel() {}

        public ActivityTypeModel(ActivityType entity) : base(entity) {}

        public string Name { get; set; }

        public bool IsEvent { get; set; }

        public bool IsClass { get; set; }

        public override void Populate(ActivityType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            IsEvent = entity.IsEvent;
            IsClass = entity.IsClass;
        }
    }
}