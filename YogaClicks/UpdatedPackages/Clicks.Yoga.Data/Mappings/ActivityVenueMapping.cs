using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ActivityVenueMapping : EntityMapping<ActivityVenue>
    {
        public ActivityVenueMapping()
        {
            HasRequired(e => e.Activity).WithOptional(e => e.ActivityVenue).Map(a => a.MapKey("ActivityId"));
            HasRequired(e => e.Venue).WithMany().Map(a => a.MapKey("VenueId"));
            Property(e => e.Confirmed).IsRequired();
        }
    }
}