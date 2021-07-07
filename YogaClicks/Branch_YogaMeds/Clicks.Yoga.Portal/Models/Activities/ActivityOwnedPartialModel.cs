using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityOwnedPartialModel
    {
        public ActivityOwnedPartialModel(Profile profile)
        {
            Profile = new EntityHandleModel(profile);
        }

        public EntityHandleModel Profile { get; set; }
    }
}