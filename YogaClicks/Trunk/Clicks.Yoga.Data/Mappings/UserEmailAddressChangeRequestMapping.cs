using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class UserEmailAddressChangeRequestMapping : EntityMapping<UserEmailAddressChangeRequest>
    {
        public UserEmailAddressChangeRequestMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            Property(e => e.ClientAddress).IsRequired().HasMaxLength(39);
            Property(e => e.Key).IsRequired().HasMaxLength(50);
            Property(e => e.EmailAddress).IsRequired().HasMaxLength(254);
            Property(e => e.ExpiryTime).IsRequired();
        }
    }
}