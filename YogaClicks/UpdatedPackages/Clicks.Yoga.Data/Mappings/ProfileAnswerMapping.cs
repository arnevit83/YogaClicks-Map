using System.ComponentModel.DataAnnotations.Schema;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ProfileAnswerMapping : EntityMapping<ProfileAnswer>
    {
        public ProfileAnswerMapping()
        {
            HasRequired(e => e.Question).WithMany().HasForeignKey(e => e.QuestionId);
            Property(e => e.Text).IsRequired();
        }

        public override void MapKey()
        {
            HasKey(e => new { e.Id, e.ProfileId, e.QuestionId });
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}