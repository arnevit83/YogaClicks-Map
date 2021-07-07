using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewRatingPartialModel
    {
        public ReviewRatingPartialModel(ReviewStatistics statistics)
        {
            Statistics = new ReviewStatisticsModel(statistics);
        }

        public ReviewStatisticsModel Statistics { get; private set; }
    }
}