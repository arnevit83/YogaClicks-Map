using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class ReviewExperienceChooserModel : MultipleEntitySelectorModel<ReviewExperience, ReviewExperienceModel>
    {
        public ReviewExperienceChooserModel() {}

        public ReviewExperienceChooserModel(IEnumerable<ReviewExperience> entities) : base(entities) { }

        public void PopulateCollection(ICollection<ReviewExperience> collection, IReviewService reviewService)
        {
            PopulateCollection(collection, reviewService.GetReviewExperience);
        }
    }
}