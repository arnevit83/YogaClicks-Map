using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class PoseCategoryMapping : EntityMapping<PoseCategory>
    {
        public PoseCategoryMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Caption).IsRequired().HasMaxLength(100);
            Property(e => e.SortKey).IsRequired();
        }
    }
}