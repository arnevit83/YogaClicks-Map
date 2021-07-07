using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewFeedbackModel
    {
        public ReviewFeedbackModel(Review review)
        {
            HelpfulCount = review.HelpfulCount;
        }

        public int HelpfulCount { get; private set; }
    }
}