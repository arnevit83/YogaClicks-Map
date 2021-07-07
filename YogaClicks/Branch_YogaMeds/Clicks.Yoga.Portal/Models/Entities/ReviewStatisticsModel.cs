using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ReviewStatisticsModel
    {
        public ReviewStatisticsModel(ReviewStatistics entity)
        {
            AverageRating = entity.AverageRating;
            Count = entity.Count;
            Exist = entity.Exist;
        }

        public decimal AverageRating { get; set; }

        public int Count { get; set; }

        public bool Exist { get; set; }
    }
}