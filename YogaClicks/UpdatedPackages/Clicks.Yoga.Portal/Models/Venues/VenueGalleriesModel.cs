using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueGalleriesModel
    {
        public VenueGalleriesModel(Venue profile)
        {
            Venue = new VenueModel(profile);
        }

        public VenueModel Venue { get; private set; }
    }
}