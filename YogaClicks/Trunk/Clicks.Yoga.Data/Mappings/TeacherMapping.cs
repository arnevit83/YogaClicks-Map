using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class TeacherMapping : EntityMapping<Teacher>
    {
        public TeacherMapping()
        {
            HasOptional(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId"));
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Philosophy);
            Property(e => e.EmailAddress).HasMaxLength(254);
            HasOptional(e => e.Location).WithMany().Map(a => a.MapKey("LocationId"));
            HasMany(e => e.Websites).WithMany().Map(a => a.MapLeftKey("TeacherId").MapRightKey("WebsiteId").ToTable("TeacherWebsite"));
            HasOptional(e => e.Blog).WithMany().Map(a => a.MapKey("BlogId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
            HasMany(e => e.Styles).WithMany().Map(a => a.MapLeftKey("TeacherId").MapRightKey("StyleId").ToTable("TeacherStyleLink"));
            HasMany(e => e.Services).WithMany().Map(a => a.MapLeftKey("TeacherId").MapRightKey("ServiceId").ToTable("TeacherServiceLink"));
            HasMany(e => e.Accreditations).WithRequired(e => e.Teacher).HasForeignKey(e => e.TeacherId);
            HasMany(e => e.Placements).WithRequired(e => e.Teacher).Map(a => a.MapKey("TeacherId"));
            Property(e => e.Stubbed).IsRequired();
        }
    }
}