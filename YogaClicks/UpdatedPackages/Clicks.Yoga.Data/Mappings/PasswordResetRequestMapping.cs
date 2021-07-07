using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class PasswordResetRequestMapping : EntityMapping<PasswordResetRequest>
    {
        public PasswordResetRequestMapping()
        {
            HasRequired(e => e.Login).WithMany().Map(a => a.MapKey("LoginId"));
            Property(e => e.Key).IsRequired().HasMaxLength(50).IsUnicode(false);
            Property(e => e.ClientAddress).IsRequired().HasMaxLength(39).IsUnicode(false);
            Property(e => e.Completed).IsRequired();
            Property(e => e.ExpiryTime).IsRequired();
        }
    }
}