using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityRepeatFrequencyModel : EntityModel<ActivityRepeatFrequency>
    {
        public ActivityRepeatFrequencyModel() { }

        public ActivityRepeatFrequencyModel(ActivityRepeatFrequency entity) : base(entity) { }

        public string Name { get; set; }

        public override void Populate(ActivityRepeatFrequency entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}