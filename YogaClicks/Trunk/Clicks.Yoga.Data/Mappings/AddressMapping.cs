using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class AddressMapping : EntityMapping<Address>
    {
        public AddressMapping()
        {
            Property(e => e.Line1).HasMaxLength(100);
            Property(e => e.Line2).HasMaxLength(100);
            Property(e => e.Line3).HasMaxLength(100);
            Property(e => e.City).HasMaxLength(100);
            Property(e => e.Area).HasMaxLength(100);
            Property(e => e.Postcode).HasMaxLength(20);
            HasOptional(e => e.Country).WithMany().Map(a => a.MapKey("CountryId"));
            Ignore(e => e.Location);
            Property(e => e.LocationGeography).HasColumnName("Location");
        }
    }
}