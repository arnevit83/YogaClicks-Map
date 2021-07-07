using System.ComponentModel.DataAnnotations;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ReviewRatingModel : EntityModel<ReviewRating>
    {
        public ReviewRatingModel(ReviewRating entity) : base(entity) {}

        public ReviewRatingSubjectModel Subject { get; set; }

        public decimal Score { get; set; }

        public string Name
        {
            get { return Subject.Name; }
        }

        public override void Populate(ReviewRating entity)
        {
            Id = entity.Id;
            Subject = new ReviewRatingSubjectModel(entity.Subject);
            Score = entity.Score;
        }
    }
}