using System.ComponentModel.DataAnnotations.Schema;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class TeacherTrainingOrganisationAccreditationMapping : EntityMapping<TeacherTrainingOrganisationAccreditation>
    {
        public TeacherTrainingOrganisationAccreditationMapping()
        {
            HasRequired(e => e.AccreditationBody).WithMany().HasForeignKey(e => e.AccreditationBodyId);
        }

        public override void MapKey()
        {
            HasKey(e => new { e.Id, e.TeacherTrainingOrganisationId, e.AccreditationBodyId });
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}