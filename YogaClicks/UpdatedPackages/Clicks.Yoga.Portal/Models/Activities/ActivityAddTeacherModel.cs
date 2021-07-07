using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityAddTeacherModel
    {
        public ActivityAddTeacherModel()
        {
            Teacher = new TeacherSelectorModel();
        }

        public TeacherSelectorModel Teacher { get; private set; }
    }
}