using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityAttendeeModel
    {
        public ActivityAttendeeModel(ActivityAttendee attendee)
        {
            Activity = new ActivityModel(attendee.Activity);
            Confirmed = attendee.Confirmed;
            IsConfirmed = attendee.IsConfirmed;
            IsPending = attendee.IsPending;
            IsRejected = attendee.IsRejected;
        }

        public ActivityModel Activity { get; private set; }

        public bool? Confirmed { get; private set; }

        public bool IsPending { get; private set; }

        public bool IsConfirmed { get; private set; }

        public bool IsRejected { get; private set; }
    }
}