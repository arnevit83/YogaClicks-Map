namespace Clicks.Yoga.Domain.Entities
{
    public class ReviewRatingSubject : Entity, IReviewDetailType
    {
        public virtual EntityType EntityType { get; set; }

        public string FilterKey { get; set; }

        public string Name { get; set; }
    }
}