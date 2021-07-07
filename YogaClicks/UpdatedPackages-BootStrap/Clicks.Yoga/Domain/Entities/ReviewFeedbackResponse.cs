namespace Clicks.Yoga.Domain.Entities
{
    public class ReviewFeedbackResponse : Entity
    {
        public virtual Review Review { get; set; }

        public virtual User User { get; set; }

        public bool Helpful { get; set; }
    }
}