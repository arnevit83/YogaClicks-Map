using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchVenueInformationModel
    {
        public SearchVenueInformationModel(Venue venue)
        {
            Venue = new VenueModel(venue);
        }
        
        public VenueModel Venue { get; private set; }
    }
}