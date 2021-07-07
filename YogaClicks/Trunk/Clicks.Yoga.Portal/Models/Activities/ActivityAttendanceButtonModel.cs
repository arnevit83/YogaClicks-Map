using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityAttendanceButtonModel
    {
        public ActivityAttendanceButtonModel(ActivityAttendanceStatus status, bool finished)
        {
            Status = status;
            Finished = finished;
        }

        public ActivityAttendanceStatus Status { get; private set; }
        public bool Finished { get; set; }
    }
}