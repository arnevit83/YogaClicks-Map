using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileGalleryModel
    {
        public ProfileGalleryModel(Profile profile, int galleryId)
        {
            Profile = new ProfileModel(profile);
            GalleryId = galleryId;
        }

        public ProfileModel Profile { get; private set; }

        public int GalleryId { get; private set; }
    }
}