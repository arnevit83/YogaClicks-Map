using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityRepeatInstance
    {
        public ActivityRepeatInstance(Activity activity, DateTime startTime, DateTime finishTime)
        {
            Activity = activity;
            StartTime = startTime;
            FinishTime = finishTime;
        }

        public Activity Activity { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime FinishTime { get; private set; }
    }
}
