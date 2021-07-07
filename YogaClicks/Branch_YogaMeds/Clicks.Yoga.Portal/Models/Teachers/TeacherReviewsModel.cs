using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherReviewsModel
    {
        public TeacherReviewsModel()
        {
            Reviews = new List<ReviewModel>();
        }

        public TeacherReviewsModel(Teacher teacher, IEnumerable<Review> reviews)
            : this()
        {
            Teacher = new TeacherModel(teacher);

            foreach (var review in reviews)
            {
                Reviews.Add(new ReviewModel(review));
            }
        }

        public TeacherModel Teacher { get; set; }

        public IList<ReviewModel> Reviews { get; private set; }
    }
}