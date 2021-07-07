using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherTrainingCourseTeacherModel : EntityModel<TeacherTrainingCourseTeacher>
    {
        public TeacherTrainingCourseTeacherModel(TeacherTrainingCourseTeacher entity) : base(entity) {}

        public TeacherModel Teacher { get; set; }

        public bool Confirmed { get; set; }

        public override void Populate(TeacherTrainingCourseTeacher entity)
        {
            Teacher = new TeacherModel(entity.Teacher);
            Confirmed = entity.Confirmed;
        }
    }
}