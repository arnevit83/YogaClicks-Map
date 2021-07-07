using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassVenueSummaryPartialModel
    {
        public ActivityClassVenueSummaryPartialModel(Activity activity)
        {
            Venue = new VenueModel(activity.ActivityVenue.Venue);
        }

        public VenueModel Venue { get; set; }
    }
}