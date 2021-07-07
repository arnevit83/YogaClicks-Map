using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class TeacherServiceMapping : EntityMapping<TeacherService>
    {
        public TeacherServiceMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}