using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityEditableListContentPartialModel
    {
        public ActivityEditableListContentPartialModel(IActor actor, bool future, IEnumerable<Activity> activities)
        {
            Actor = new EntityHandleModel(actor);
            Future = future;
            Activities = new List<ActivityModel>(activities.Select(a => new ActivityModel(a)));
        }

        public ActivityEditableListContentPartialModel(EntityHandleModel actor, bool future, IEnumerable<ActivityModel> activities)
        {
            Actor = actor;
            Future = future;
            Activities = new List<ActivityModel>(activities);
        }

        public EntityHandleModel Actor { get; private set; }

        public bool Future { get; set; }

        public IList<ActivityModel> Activities { get; private set; }

        public bool HidePromoter { get; set; }
    }
}