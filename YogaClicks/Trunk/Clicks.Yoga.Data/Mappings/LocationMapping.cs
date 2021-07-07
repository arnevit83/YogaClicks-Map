using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class LocationMapping : EntityMapping<Location>
    {
        public LocationMapping()
        {
            Property(e => e.Name).HasMaxLength(100);
            Ignore(e => e.Coordinates);
            Property(e => e.Geography).HasColumnName("Geography");
        }
    }
}