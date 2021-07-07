using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class ReviewSearchCriteria
    {
        public ReviewSearchCriteria()
        {
            Experiences = new  List<ReviewExperience>();
        }

        public IReviewable Subject { get; set; }

        public IEntityHandle Parent { get; set; }

        public IActor Author { get; set; }

        public ICollection<ReviewExperience> Experiences { get; private set; }

        public ReviewSortOrder SortOrder { get; set; }

        public bool Any
        {
            get { return Subject != null || Parent != null || Author != null; }
        }
    }

    public enum ReviewSortOrder
    {
        MostRecent,
        MostHelpful,
        HighestRated
    }
}