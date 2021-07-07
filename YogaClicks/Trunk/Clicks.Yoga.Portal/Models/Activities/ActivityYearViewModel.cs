using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityYearViewModel
    {
        public ActivityYearViewModel(List<Activity> events)
        {
            Events = new List<ActivityModel>();

            foreach (var ycEvent in events)
            {
                Events.Add(new ActivityModel(ycEvent));  
            }
        }

        public List<ActivityModel> Events { get; private set; }
    }
}