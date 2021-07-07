using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ReviewMapping : PrincipalEntityMapping<Review>
    {
        public ReviewMapping()
        {
            HasRequired(e => e.AuthorHandle).WithMany().Map(a => a.MapKey("AuthorHandleId")).WillCascadeOnDelete(false);
            HasRequired(e => e.SubjectHandle).WithMany().Map(a => a.MapKey("SubjectHandleId")).WillCascadeOnDelete(false);
            Property(e => e.Headline).IsRequired().HasMaxLength(100);
            Property(e => e.Body).IsRequired();
            Property(e => e.Rating).IsRequired().HasPrecision(2, 1);
            HasMany(e => e.ReviewedExperiences).WithMany(e => e.Reviews).Map(a => a.MapLeftKey("ReviewId").MapRightKey("TypeId").ToTable("ReviewExperienceLink"));
            HasMany(e => e.DetailedRatings).WithRequired(e => e.Review).HasForeignKey(e => e.ReviewId);
            HasMany(e => e.Comments).WithMany().Map(a => a.MapLeftKey("ReviewId").MapRightKey("CommentId").ToTable("ReviewComment"));
            Property(e => e.HelpfulCount).IsRequired();
        }
    }
}