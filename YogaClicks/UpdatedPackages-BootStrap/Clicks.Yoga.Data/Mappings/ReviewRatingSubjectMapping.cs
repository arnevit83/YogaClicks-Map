using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ReviewRatingSubjectMapping : EntityMapping<ReviewRatingSubject>
    {
        public ReviewRatingSubjectMapping()
        {
            HasRequired(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId")).WillCascadeOnDelete(false);
            Property(e => e.FilterKey).HasMaxLength(100);
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}