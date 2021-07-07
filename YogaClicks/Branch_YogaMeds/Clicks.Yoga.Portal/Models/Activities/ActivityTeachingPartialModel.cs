using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityTeachingPartialModel
    {
        public ActivityTeachingPartialModel(Teacher teacher, IEnumerable<Activity> activities)
        {
            Teacher = new TeacherModel(teacher);
            Activities = new List<ActivityModel>(activities.Select(a => new ActivityModel(a)));
        }

        public TeacherModel Teacher { get; private set; }

        public IList<ActivityModel> Activities { get; private set; }
    }
}