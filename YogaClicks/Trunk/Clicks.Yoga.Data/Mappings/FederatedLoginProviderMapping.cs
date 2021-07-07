using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class FederatedLoginProviderMapping : EntityMapping<FederatedLoginProvider>
    {
        public FederatedLoginProviderMapping()
        {
            Property(e => e.Tag).IsRequired();
            Property(e => e.Name).IsRequired();
            Property(e => e.IsFacebook).IsRequired();
        }
    }
}