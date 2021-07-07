using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    class TeacherTrainingCourseTeacherMapping : EntityMapping<TeacherTrainingCourseTeacher>
    {
        public TeacherTrainingCourseTeacherMapping()
        {
            HasRequired(e => e.Course).WithMany(e => e.Teachers).Map(a => a.MapKey("CourseId"));
            HasRequired(e => e.Teacher).WithMany().Map(a => a.MapKey("TeacherId"));
            Property(e => e.Confirmed).IsRequired();
        }
    }
}