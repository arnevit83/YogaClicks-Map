using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class StyleTraitGroupMapping : EntityMapping<StyleTraitGroup>
    {
        public StyleTraitGroupMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}