using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewListPartialModel
    {
        public ReviewListPartialModel()
        {
            Reviews = new List<ReviewModel>();
        }

        public ReviewListPartialModel(IEnumerable<Review> reviews, bool showSubject, bool showChild)
            : this()
        {
            ShowSubject = showSubject;

            ShowChild = showChild;

            foreach (var review in reviews)
            {
                Reviews.Add(new ReviewModel(review));
            }

            AverageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;
        }

        public bool ShowSubject { get; private set; }

        public bool ShowChild { get; private set; }

        public IList<ReviewModel> Reviews { get; private set; }

        [UIHint("Rating")]
        public decimal AverageRating { get; private set; }
    }
}