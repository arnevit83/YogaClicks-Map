using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityRepeatInstanceModel
    {
        public ActivityRepeatInstanceModel(ActivityRepeatInstance repeat, ActivityModel activity)
        {
            Activity = activity;
            StartTime = repeat.StartTime;
            FinishTime = repeat.FinishTime;
        }

        public ActivityModel Activity { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime FinishTime { get; private set; }
    }
}