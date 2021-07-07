using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityGalleriesModel
    {
        public ActivityGalleriesModel(Activity profile)
        {
            Activity = new ActivityModel(profile);
        }

        public ActivityModel Activity { get; private set; }
    }
}