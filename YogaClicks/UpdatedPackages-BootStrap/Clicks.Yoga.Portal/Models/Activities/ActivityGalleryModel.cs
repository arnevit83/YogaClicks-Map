using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityGalleryModel
    {
        public ActivityGalleryModel(Activity profile, int galleryId)
        {
            Activity = new ActivityModel(profile);
            GalleryId = galleryId;
        }

        public ActivityModel Activity { get; private set; }

        public int GalleryId { get; private set; }
    }
}