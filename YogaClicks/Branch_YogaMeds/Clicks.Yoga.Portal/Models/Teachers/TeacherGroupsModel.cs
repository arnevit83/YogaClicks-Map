using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherGroupsModel
    {
        public TeacherGroupsModel(Teacher teacher)
        {
            Teacher = new TeacherModel(teacher);
        }

        public TeacherModel Teacher { get; private set; }
    }
}