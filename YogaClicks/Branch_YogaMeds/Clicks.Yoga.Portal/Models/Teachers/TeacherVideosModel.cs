using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherVideosModel
    {
        public TeacherVideosModel(Teacher profile)
        {
            Teacher = new TeacherModel(profile);
        }

        public TeacherModel Teacher { get; private set; }
    }
}