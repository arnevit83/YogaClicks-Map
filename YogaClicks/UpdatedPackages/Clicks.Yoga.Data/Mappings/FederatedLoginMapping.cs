using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class FederatedLoginMapping : EntityMapping<FederatedLogin>
    {
        public FederatedLoginMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            HasRequired(e => e.Provider).WithMany().Map(a => a.MapKey("ProviderId"));
            Property(e => e.Identifier).IsRequired();
        }
    }
}