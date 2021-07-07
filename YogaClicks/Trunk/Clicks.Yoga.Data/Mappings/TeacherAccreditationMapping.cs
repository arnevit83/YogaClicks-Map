using System.ComponentModel.DataAnnotations.Schema;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class TeacherAccreditationMapping : EntityMapping<TeacherAccreditation>
    {
        public TeacherAccreditationMapping()
        {
            HasRequired(e => e.AccreditationBody).WithMany().HasForeignKey(e => e.AccreditationBodyId);
        }

        public override void MapKey()
        {
            HasKey(e => new { e.Id, e.TeacherId, e.AccreditationBodyId });
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}