using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class StyleOrganisationMapping : PrincipalEntityMapping<StyleOrganisation>
    {
        public StyleOrganisationMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            HasRequired(e => e.Style).WithMany().Map(a => a.MapKey("StyleId"));
            Property(e => e.Founder).HasMaxLength(100);
            Property(e => e.FoundedYear);
            Property(e => e.EmailAddress);
            Property(e => e.Telephone);
            HasOptional(e => e.Website).WithMany().Map(a => a.MapKey("WebsiteId"));
            HasRequired(e => e.Location).WithMany().Map(a => a.MapKey("LocationId"));
            Property(e => e.Description);
            Property(e => e.Affiliations);
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
        }
    }
}