using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityProfessionalsModel
    {
        public ActivityProfessionalsModel(Activity activity, IEnumerable<ActivityTeacher> teachers)
        {
            Activity = new ActivityModel(activity);
            Teachers = new List<ActivityTeacherModel>();

            foreach (var teacher in teachers)
            {
                Teachers.Add(new ActivityTeacherModel(teacher, Activity));
            }
        }

        public ActivityModel Activity { get; private set; }

        public IList<ActivityTeacherModel> Teachers { get; private set; }
    }
}