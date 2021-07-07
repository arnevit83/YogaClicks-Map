using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ActivityTeacherModel
    {
        public ActivityTeacherModel(ActivityTeacher entity, ActivityModel activity)
        {
            Activity = activity;
            Teacher = new TeacherModel(entity.Teacher);
            Confirmed = entity.Confirmed;
        }

        public ActivityTeacherModel(ActivityTeacher entity, TeacherModel teacher)
        {
            Activity = new ActivityModel(entity.Activity);
            Teacher = teacher;
            Confirmed = entity.Confirmed;
        }

        public ActivityModel Activity { get; private set; }

        public TeacherModel Teacher { get; private set; }

        public bool Confirmed { get; private set; }
    }
}