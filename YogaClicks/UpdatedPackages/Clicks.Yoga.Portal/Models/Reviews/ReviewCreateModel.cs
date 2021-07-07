using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewCreateModel : ReviewEditorModel
    {
        public ReviewCreateModel() {}

        public ReviewCreateModel(IActor author, IReviewable subject) : base(subject)
        {
            Author = new EntityReference(author.Id, author.GetEntityTypeName());
            Subject = new EntityHandleModel(subject);
        }
    }
}