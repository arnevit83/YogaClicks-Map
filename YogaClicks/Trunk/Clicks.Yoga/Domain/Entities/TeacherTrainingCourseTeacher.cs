namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherTrainingCourseTeacher : Entity
    {
        public TeacherTrainingCourseTeacher() {}

        public TeacherTrainingCourseTeacher(Teacher teacher)
        {
            Teacher = teacher;
        }

        public virtual TeacherTrainingCourse Course { get; set; }

        public virtual Teacher Teacher { get; set; }

        public bool Confirmed { get; set; }
    }
}