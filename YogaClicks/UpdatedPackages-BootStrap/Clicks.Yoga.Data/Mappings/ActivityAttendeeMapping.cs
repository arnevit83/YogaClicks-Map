using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ActivityAttendeeMapping : EntityMapping<ActivityAttendee>
    {
        public ActivityAttendeeMapping()
        {
            HasRequired(e => e.Activity).WithMany(e => e.Attendees).Map(a => a.MapKey("ActivityId"));
            HasRequired(e => e.Handle).WithMany().Map(a => a.MapKey("HandleId")).WillCascadeOnDelete(false);
            Property(e => e.Confirmed).IsOptional();
        }
    }
}