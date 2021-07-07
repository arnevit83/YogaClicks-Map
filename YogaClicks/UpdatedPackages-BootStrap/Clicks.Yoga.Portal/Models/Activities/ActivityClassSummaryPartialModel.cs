using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassSummaryPartialModel
    {
        public ActivityClassSummaryPartialModel(Activity activity)
        {
            Activity = new ActivityModel(activity);
            Styles = new List<StyleModel>();

            foreach (var style in activity.Styles)
            {
                Styles.Add(new StyleModel(style));
            }
        }

        public ActivityModel Activity { get; private set; }

        public IList<StyleModel> Styles { get; private set; }
    }
}