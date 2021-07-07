using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class UserEmailAddressChangeActionMapping : EntityMapping<UserEmailAddressChangeAction>
    {
        public UserEmailAddressChangeActionMapping()
        {
            HasRequired(e => e.Request).WithOptional(e => e.Action).Map(a => a.MapKey("RequestId"));
            Property(e => e.ClientAddress).IsRequired().HasMaxLength(39);
        }
    }
}