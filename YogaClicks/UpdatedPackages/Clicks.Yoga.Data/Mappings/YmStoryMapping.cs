using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class YmStoryMapping : EntityMapping<YmStory>
    {
        public YmStoryMapping()
        {
            
            Property(e => e.Name).IsRequired().HasMaxLength(100);

            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));

            HasMany(e => e.Conditions).WithMany().Map(a => a.MapLeftKey("YmStoryId").MapRightKey("ConditionId").ToTable("YmStoryConditionLink"));
            HasMany(e => e.Teachers).WithMany().Map(a => a.MapLeftKey("YmStoryId").MapRightKey("TeacherId").ToTable("YmStoryTeacherLink"));
            HasMany(e => e.Venues).WithMany().Map(a => a.MapLeftKey("YmStoryId").MapRightKey("VenueId").ToTable("YmStoryVenueLink"));
            HasMany(e => e.LifeChallenges).WithMany().Map(a => a.MapLeftKey("YmStoryId").MapRightKey("YmStoryLifeChallengeId").ToTable("YmStoryLifeChallengeLink"));
            //HasOptional(e => e.Condition).WithMany().Map(a => a.MapKey("ConditionId"));
            HasOptional(e => e.Address).WithMany().Map(a => a.MapKey("AddressId"));
            Property(e => e.PoweredFrom);
            Property(e => e.ProfileType).IsRequired().HasMaxLength(20);
            Property(e => e.Story);
            Property(e => e.Title);
            Property(e => e.Video).HasMaxLength(100);
            HasOptional(e => e.VideoUpload).WithMany().Map(a => a.MapKey("VideoId"));
            Property(e => e.Lat).IsRequired();
            Property(e => e.Lng).IsRequired();
            Property(e => e.ShopUrl);
            HasRequired(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId")).WillCascadeOnDelete(false);

        }
    }
}
