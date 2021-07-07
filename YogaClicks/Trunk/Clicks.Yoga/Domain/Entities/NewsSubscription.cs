namespace Clicks.Yoga.Domain.Entities
{
    public class NewsSubscription : Entity
    {
        public virtual Profile Subscriber { get; set; }

        public virtual EntityHandle Subject { get; set; }
    }
}