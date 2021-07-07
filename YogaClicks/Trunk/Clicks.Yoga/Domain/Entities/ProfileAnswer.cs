namespace Clicks.Yoga.Domain.Entities
{
    public class ProfileAnswer : Entity
    {
        public int ProfileId { get; set; }

        public int QuestionId { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual ProfileQuestion Question { get; set; }

        public string Text { get; set; }
    }
}