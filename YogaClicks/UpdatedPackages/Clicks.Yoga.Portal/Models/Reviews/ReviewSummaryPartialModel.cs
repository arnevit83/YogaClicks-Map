using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewSummaryPartialModel
    {
        public ReviewSummaryPartialModel(ReviewSearchCriteriaModel criteria, ReviewStatistics statistics, IReviewable subject)
        {
            Criteria = criteria;
            Statistics = new ReviewStatisticsModel(statistics);
            Subject = new EntityHandleModel(subject);
        }

        public ReviewSearchCriteriaModel Criteria { get; private set; }

        public ReviewStatisticsModel Statistics { get; private set; }

        public EntityHandleModel Subject { get; private set; }
    }
}