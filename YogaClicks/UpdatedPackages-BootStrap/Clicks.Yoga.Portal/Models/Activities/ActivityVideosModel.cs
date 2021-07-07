using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityVideosModel
    {
        public ActivityVideosModel(Activity profile)
        {
            Activity = new ActivityModel(profile);
        }

        public ActivityModel Activity { get; private set; }
    }
}