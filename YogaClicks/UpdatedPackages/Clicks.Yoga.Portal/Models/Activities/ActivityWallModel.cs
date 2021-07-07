using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityWallModel
    {
        public ActivityWallModel(Activity activity)
        {
            Activity = new ActivityModel(activity);
        }

        public ActivityModel Activity { get; private set; }
    }
}