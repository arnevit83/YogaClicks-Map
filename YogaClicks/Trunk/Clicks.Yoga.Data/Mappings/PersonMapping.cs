using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ProfileMapping : EntityMapping<Profile>
    {
        public ProfileMapping()
        {
            HasRequired(e => e.Owner).WithOptional(e => e.Profile).Map(a => a.MapKey("OwnerId"));
            Property(e => e.Forename).IsRequired().HasMaxLength(50);
            Property(e => e.Surname).IsRequired().HasMaxLength(50);
            HasOptional(e => e.Gender).WithMany().Map(a => a.MapKey("GenderId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
            Property(e => e.BirthDate);
            Property(e => e.EmailAddress).HasMaxLength(254);
            HasMany(e => e.Websites).WithMany().Map(a => a.MapLeftKey("ProfileId").MapRightKey("WebsiteId").ToTable("ProfileWebsiteLink"));
            HasMany(e => e.ProfileAnswers).WithRequired(e => e.Profile).HasForeignKey(e => e.ProfileId);
            HasOptional(e => e.Location).WithMany().Map(a => a.MapKey("LocationId"));
            Property(e => e.Public).IsRequired();
        }
    }
}