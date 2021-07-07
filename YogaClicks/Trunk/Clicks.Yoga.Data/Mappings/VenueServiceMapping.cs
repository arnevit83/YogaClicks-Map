using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class VenueServiceMapping : EntityMapping<VenueService>
    {
        public VenueServiceMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}