using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class ReviewExperience : Entity, IReviewDetailType
    {
        public ReviewExperience()
        {
            Reviews = new List<Review>();
        }

        public virtual EntityType EntityType { get; set; }

        public string FilterKey { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Review> Reviews { get; private set; }
    }
}