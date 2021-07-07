using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    class TeacherTrainingOrganisationMapping : EntityMapping<TeacherTrainingOrganisation>
    {
        public TeacherTrainingOrganisationMapping()
        {
            HasOptional(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId"));
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
            Property(e => e.Founder).HasMaxLength(100);
            Property(e => e.Description);
            HasMany(e => e.Styles).WithMany().Map(a => a.MapLeftKey("OrganisationId").MapRightKey("StyleId").ToTable("TeacherTrainingOrganisationStyleLink"));
            HasOptional(e => e.Address).WithMany().Map(a => a.MapKey("AddressId"));
            HasMany(e => e.Courses).WithRequired(e => e.Organisation).Map(a => a.MapKey("OrganisationId"));
            HasOptional(e => e.Website).WithMany().Map(a => a.MapKey("WebsiteId"));
            Property(e => e.EmailAddress).IsOptional().HasMaxLength(255);
        }
    }
}
