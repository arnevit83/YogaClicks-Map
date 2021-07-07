using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Medic;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassSummaryPartialModel
    {
        public ActivityClassSummaryPartialModel(Activity activity)
        {
            Activity = new ActivityModel(activity);
            Styles = new List<StyleModel>();
            Conditions = new List<ConditionModel>();

            foreach (var style in activity.Styles)
            {
                Styles.Add(new StyleModel(style));
            }

            foreach (var condition in activity.Conditions)
            {
                Conditions.Add(new ConditionModel(condition));
            }
        }

        public ActivityModel Activity { get; private set; }

        public IList<StyleModel> Styles { get; private set; }

        public IList<ConditionModel> Conditions { get; private set; }
    }
}