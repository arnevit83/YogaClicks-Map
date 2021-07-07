using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class VenueTypeMapping : EntityMapping<VenueType>
    {
        public VenueTypeMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(50);
            Property(e => e.IsResidential).IsRequired();
        }
    }
}