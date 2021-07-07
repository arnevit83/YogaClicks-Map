using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityHostingPartialModel
    {
        public ActivityHostingPartialModel(Venue venue, IEnumerable<Activity> activities)
        {
            Venue = new VenueModel(venue);
            Activities = new List<ActivityModel>(activities.Select(a => new ActivityModel(a)));
        }

        public VenueModel Venue { get; private set; }

        public IList<ActivityModel> Activities { get; private set; }
    }
}