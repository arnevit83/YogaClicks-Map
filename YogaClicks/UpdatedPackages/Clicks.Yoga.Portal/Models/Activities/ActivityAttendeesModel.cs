using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityAttendeesModel
    {
        public ActivityAttendeesModel(Activity activity, IEnumerable<ActivityAttendee> attendees)
        {
            Activity = new ActivityModel(activity);
            ConfirmedAttendees = new List<EntityHandleModel>();
            UnconfirmedAttendees = new List<EntityHandleModel>();
            RejectedAttendees = new List<EntityHandleModel>();

            foreach (var attendee in attendees)
            {
                var handle = new EntityHandleModel(attendee.Handle);
                var list = UnconfirmedAttendees;

                if (attendee.IsConfirmed)
                    list = ConfirmedAttendees;
                else if (attendee.IsRejected)
                    list = RejectedAttendees;

                list.Add(handle);
            }
        }

        public ActivityModel Activity { get; private set; }

        public IList<EntityHandleModel> ConfirmedAttendees { get; private set; }

        public IList<EntityHandleModel> UnconfirmedAttendees { get; private set; }

        public IList<EntityHandleModel> RejectedAttendees { get; private set; }
    }
}