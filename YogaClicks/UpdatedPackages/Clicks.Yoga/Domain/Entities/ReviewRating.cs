namespace Clicks.Yoga.Domain.Entities
{
    public class ReviewRating : Entity
    {
        public int ReviewId { get; set; }

        public int SubjectId { get; set; }

        public virtual Review Review { get; set; }

        public virtual ReviewRatingSubject Subject { get; set; }

        public decimal Score { get; set; }

        public string Name
        {
            get { return Subject.Name; }
        }
    }
}