using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueReviewsModel
    {
        public VenueReviewsModel()
        {
            Reviews = new List<ReviewModel>();
        }

        public VenueReviewsModel(Venue venue, IEnumerable<Review> reviews)
            : this()
        {
            Venue = new VenueModel(venue);

            foreach (var review in reviews)
            {
                Reviews.Add(new ReviewModel(review));
            }
        }

        public VenueModel Venue { get; set; }

        public IList<ReviewModel> Reviews { get; private set; }
    }
}