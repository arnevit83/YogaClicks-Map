using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class AccreditationBodyMapping : PrincipalEntityMapping<AccreditationBody>
    {
        public AccreditationBodyMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Abbreviation).HasMaxLength(10);
            Property(e => e.Description).IsRequired();
            Property(e => e.Programmes);
            HasOptional(e => e.Address).WithMany().Map(a => a.MapKey("AddressId"));
            HasOptional(e => e.Website).WithMany().Map(a => a.MapKey("WebsiteId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasMany(e => e.Teachers).WithRequired(e => e.AccreditationBody).HasForeignKey(e => e.AccreditationBodyId);
            HasMany(e => e.Accreditations).WithRequired(e => e.AccreditationBody).Map(a => a.MapKey("AccreditationBodyId"));
        }
    }
}