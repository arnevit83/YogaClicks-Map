using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class YmStoryLifeChallengesMapping : EntityMapping<YmStoryLifeChallenge>
    {
        public YmStoryLifeChallengesMapping()
        {
            
            Property(e => e.Name).IsRequired().HasMaxLength(100);


   

            HasMany(e => e.LifeChallengebucket).WithMany().Map(a => a.MapLeftKey("YmStoryLifeChallengeBucketId").MapRightKey("YmStoryLifeChallengeId").ToTable("YmStoryLifeChallengeBucketLink"));

        
        }
    }
}
