using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.News
{
    public class NewsReviewPartialModel
    {
        public NewsReviewPartialModel(Review review)
        {
            Rating = review.Rating;
            Body = review.Body;
        }

        public decimal Rating { get; set; }

        public string Body { get; set; }
    }
}