using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassTeacherSummaryPartialModel
    {
        public ActivityClassTeacherSummaryPartialModel(Activity activity)
        {
            Teacher = new TeacherModel(activity.Teachers.Count > 0 ? activity.Teachers.First().Teacher : null);
        }

        public TeacherModel Teacher { get; set; }
    }
}