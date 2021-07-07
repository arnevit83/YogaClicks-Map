using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherGalleriesModel
    {
        public TeacherGalleriesModel(Teacher profile)
        {
            Teacher = new TeacherModel(profile);
        }

        public TeacherModel Teacher { get; private set; }
    }
}