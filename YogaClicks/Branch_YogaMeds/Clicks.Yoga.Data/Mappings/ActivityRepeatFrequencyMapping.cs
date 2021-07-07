using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ActivityRepeatFrequencyMapping : EntityMapping<ActivityRepeatFrequency>
    {
        public ActivityRepeatFrequencyMapping()
        {
            Property(e => e.Name).IsRequired();
        }
    }
}