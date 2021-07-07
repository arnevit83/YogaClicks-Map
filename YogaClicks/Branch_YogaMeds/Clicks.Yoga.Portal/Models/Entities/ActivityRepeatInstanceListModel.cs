using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityRepeatInstanceListModel : List<ActivityRepeatInstanceModel>
    {
        public ActivityRepeatInstanceListModel(IList<ActivityRepeatInstance> repeats)
        {
            var activities = new Dictionary<int, ActivityModel>();

            foreach (var activity in repeats.Select(r => r.Activity).Distinct())
            {
                if (activities.ContainsKey(activity.Id)) continue;
                activities.Add(activity.Id, new ActivityModel(activity));
            }

            foreach (var repeat in repeats)
            {
                Add(new ActivityRepeatInstanceModel(repeat, activities[repeat.Activity.Id]));
            }
        } 
    }
}