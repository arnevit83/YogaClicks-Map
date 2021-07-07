using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewDisplayModel
    {
        public ReviewDisplayModel(Review review)
        {
            Review = new ReviewModel(review);
        }

        public ReviewModel Review { get; private set; }
    }
}