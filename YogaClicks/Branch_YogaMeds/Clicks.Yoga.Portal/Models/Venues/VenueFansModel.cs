using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueFansModel
    {
        public VenueFansModel(Venue venue)
        {
            Venue = new VenueModel(venue);
        }

        public VenueModel Venue { get; private set; }
    }
}