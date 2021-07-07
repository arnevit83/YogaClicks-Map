using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityAttendingPartialModel
    {
        public ActivityAttendingPartialModel(IActor attendee, IEnumerable<ActivityAttendee> attendances)
        {
            Attendee = new EntityHandleModel(attendee);
            Attendances = new List<ActivityAttendeeModel>(attendances.Select(a => new ActivityAttendeeModel(a)));
        }

        public EntityHandleModel Attendee { get; private set; }

        public IList<ActivityAttendeeModel> Attendances { get; private set; }
    }
}