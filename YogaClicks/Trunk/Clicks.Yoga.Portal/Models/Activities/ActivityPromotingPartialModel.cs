using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityPromotingPartialModel
    {
        public ActivityPromotingPartialModel(EntityReference actor)
        {
            Actor = actor;
        }

        public EntityReference Actor { get; private set; }
    }
}