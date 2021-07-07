using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewUpdateModel : ReviewEditorModel
    {
        public ReviewUpdateModel() {}

        public ReviewUpdateModel(Review review, IReviewable subject) : base(review, subject) {}
    }
}