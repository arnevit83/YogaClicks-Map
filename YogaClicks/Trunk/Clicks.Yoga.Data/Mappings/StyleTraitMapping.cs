using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class StyleTraitMapping : EntityMapping<StyleTrait>
    {
        public StyleTraitMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            HasRequired(e => e.Group).WithMany(e => e.Traits).Map(a => a.MapKey("GroupId"));
        }
    }
}