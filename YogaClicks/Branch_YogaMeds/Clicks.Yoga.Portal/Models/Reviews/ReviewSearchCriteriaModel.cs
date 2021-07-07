using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewSearchCriteriaModel
    {
        public ReviewSearchCriteriaModel()
        {
            Experiences = new ReviewExperienceChooserModel();
        }

        public int? SubjectId { get; set; }

        public string SubjectType { get; set; }

        public int? ParentId { get; set; }

        public string ParentType { get; set; }

        public int? AuthorId { get; set; }

        public string AuthorType { get; set; }

        public ReviewExperienceChooserModel Experiences { get; set; }

        public ReviewSortOrder SortOrder { get; set; }

        public string Title { get; set; }

        public void PopulateEntity(
            ReviewSearchCriteria entity,
            IReviewService reviewService,
            IEntityService entityService)
        {
            if (SubjectId.HasValue && !string.IsNullOrEmpty(SubjectType))
            {
                entity.Subject = entityService.GetEntity<IReviewable>(SubjectId.Value, SubjectType);
            }

            if (ParentId.HasValue && !string.IsNullOrEmpty(ParentType))
            {
                entity.Parent = entityService.GetEntity<IEntityHandle>(ParentId.Value, ParentType);
            }

            if (AuthorId.HasValue && !string.IsNullOrEmpty(AuthorType))
            {
                entity.Author = entityService.GetEntity<IActor>(AuthorId.Value, AuthorType);
            }

            Experiences.PopulateCollection(entity.Experiences, reviewService);

            entity.SortOrder = SortOrder;
        }

        public void PopulateMetadata(IEnumerable<ReviewExperience> experiences)
        {
            Experiences.PopulateMetadata(experiences);
        }
    }
}