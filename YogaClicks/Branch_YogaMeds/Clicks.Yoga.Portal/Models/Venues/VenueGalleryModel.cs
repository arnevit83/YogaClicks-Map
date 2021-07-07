using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueGalleryModel
    {
        public VenueGalleryModel(Venue profile, int galleryId)
        {
            Venue = new VenueModel(profile);
            GalleryId = galleryId;
        }

        public VenueModel Venue { get; private set; }

        public int GalleryId { get; private set; }
    }
}