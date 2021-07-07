using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class YmStoryLifeChallengesBucketMapping : EntityMapping<YmStoryLifeChallengeBucket>
    {
        public YmStoryLifeChallengesBucketMapping()
        {
            
            Property(e => e.Name).IsRequired().HasMaxLength(100);


   

            
        }
    }
}
