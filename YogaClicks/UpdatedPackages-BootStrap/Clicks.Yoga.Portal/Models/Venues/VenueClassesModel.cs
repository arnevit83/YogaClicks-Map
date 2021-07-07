using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueClassesModel
    {
        public VenueClassesModel(Venue venue)
        {
            Venue = new VenueModel(venue);
        }

        public VenueModel Venue { get; private set; }
    }
}