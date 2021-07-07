using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class ActivityTeacherMapping : EntityMapping<ActivityTeacher>
    {
        public ActivityTeacherMapping()
        {
            HasRequired(e => e.Activity).WithMany(e => e.Teachers).Map(a => a.MapKey("ActivityId"));
            HasRequired(e => e.Teacher).WithMany().Map(a => a.MapKey("TeacherId"));
            Property(e => e.Confirmed).IsRequired();
        }
    }
}