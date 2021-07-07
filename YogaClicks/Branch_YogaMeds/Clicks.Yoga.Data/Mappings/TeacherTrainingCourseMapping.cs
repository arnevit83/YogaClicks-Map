using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    class TeacherTrainingCourseMapping : EntityMapping<TeacherTrainingCourse>
    {
        public TeacherTrainingCourseMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
            Property(e => e.Duration);
            Property(e => e.StartDate);
            Property(e => e.FinishDate);
            HasOptional(e => e.Style).WithMany().Map(a => a.MapKey("StyleId"));
            HasOptional(e => e.Website).WithMany().Map(a => a.MapKey("WebsiteId"));
            HasOptional(e => e.Image).WithMany().Map(a => a.MapKey("ImageId"));
            HasMany(e => e.AccreditationBodies).WithMany().Map(a => a.ToTable("TeacherTrainingCourseAccreditations").MapLeftKey("CourseId").MapRightKey("AccreditationBodyId"));
        }
    }
}
