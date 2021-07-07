using System.Collections.Generic;
using System.Linq;

namespace Clicks.Yoga.Domain.Entities
{
    public class ReviewStatistics
    {
        public ReviewStatistics(decimal averageRating, int count)
        {
            AverageRating = averageRating;
            Count = count;
        }

        public ReviewStatistics(ICollection<Review> reviews)
        {
            AverageRating = reviews.Average(r => r.Rating);
            Count = reviews.Count();
        }

        public decimal AverageRating { get; private set; }

        public int Count { get; private set; }

        public bool Exist
        {
            get { return Count > 0; }
        }
    }
}