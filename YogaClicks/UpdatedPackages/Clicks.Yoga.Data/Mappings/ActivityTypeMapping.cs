using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ActivityTypeMapping : EntityMapping<ActivityType>
    {
        public ActivityTypeMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}