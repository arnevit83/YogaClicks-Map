using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ActivityMapping : PrincipalEntityMapping<Activity>
    {
        public ActivityMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            HasRequired(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            HasRequired(e => e.PromoterHandle).WithMany().Map(a => a.MapKey("PromoterHandleId"));
            HasOptional(e => e.AbilityLevel).WithMany().Map(a => a.MapKey("AbilityLevelId"));
            Property(e => e.Description).IsRequired();
            Property(e => e.StartTime).IsRequired();
            Property(e => e.FinishTime).IsRequired();
            HasOptional(e => e.RepeatFrequency).WithMany().Map(a => a.MapKey("RepeatFrequencyId"));
            Property(e => e.BookingRequired).IsRequired();
            Property(e => e.Price).IsRequired();
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasOptional(e => e.ProfileBanner).WithMany().Map(a => a.MapKey("ProfileBannerImageId"));
            HasMany(e => e.Styles).WithMany().Map(a => a.MapLeftKey("ActivityId").MapRightKey("StyleId").ToTable("ActivityStyle"));
            Property(e => e.Public).IsRequired();
            Property(e => e.AttendeeInvitationsPermitted).IsRequired();
            Property(e => e.AttendeePostingPermitted).IsRequired();
            Property(e => e.AttendeeGalleriesPermitted).IsRequired();
            Property(e => e.ProductDescription);
            Property(e => e.ProductButton);
        }
    }
}