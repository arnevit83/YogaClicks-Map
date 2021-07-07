using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class AbilityLevelMapping : EntityMapping<AbilityLevel>
    {
        public AbilityLevelMapping()
        {
            Property(e => e.Index).IsRequired();
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}