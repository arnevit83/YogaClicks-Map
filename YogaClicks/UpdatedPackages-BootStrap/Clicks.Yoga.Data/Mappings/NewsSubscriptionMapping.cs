using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NewsSubscriptionMapping : EntityMapping<NewsSubscription>
    {
        public NewsSubscriptionMapping()
        {
            HasRequired(e => e.Subscriber).WithMany().Map(a => a.MapKey("SubscriberProfileId")).WillCascadeOnDelete(false);
            HasRequired(e => e.Subject).WithMany().Map(a => a.MapKey("SubjectHandleId")).WillCascadeOnDelete(false);
        }
    }
}