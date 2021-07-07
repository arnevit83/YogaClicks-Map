using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class AbilityLevelModel : EntityModel<AbilityLevel>
    {
        public AbilityLevelModel() {}

        public AbilityLevelModel(AbilityLevel entity) : base(entity) {}

        public byte Index { get; set; }

        public string Name { get; set; }

        public override void Populate(AbilityLevel entity)
        {
            Id = entity.Id;
            Index = entity.Index;
            Name = entity.Name;
        }
    }
}