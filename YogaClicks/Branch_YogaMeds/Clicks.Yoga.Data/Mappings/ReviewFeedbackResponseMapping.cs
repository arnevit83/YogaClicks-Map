using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ReviewFeedbackResponseMapping : EntityMapping<ReviewFeedbackResponse>
    {
        public ReviewFeedbackResponseMapping()
        {
            HasRequired(e => e.Review).WithMany().Map(a => a.MapKey("ReviewId")).WillCascadeOnDelete(false);
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId")).WillCascadeOnDelete(false);
            Property(e => e.Helpful).IsRequired();
        }
    }
}