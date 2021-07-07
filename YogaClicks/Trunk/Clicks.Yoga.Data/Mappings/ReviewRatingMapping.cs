using System.ComponentModel.DataAnnotations.Schema;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ReviewRatingMapping : EntityMapping<ReviewRating>
    {
        public ReviewRatingMapping()
        {
            HasRequired(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId);
            Property(e => e.Score).IsRequired().HasPrecision(2, 1);
            Ignore(e => e.Name);
        }

        public override void MapKey()
        {
            HasKey(e => new { e.Id, e.ReviewId, e.SubjectId });
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}