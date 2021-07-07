using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class PasswordLoginMapping : EntityMapping<PasswordLogin>
    {
        public PasswordLoginMapping()
        {
            HasRequired(e => e.User).WithMany(e => e.PasswordLogins).Map(a => a.MapKey("UserId"));
            Property(e => e.Username).IsRequired().HasMaxLength(254);
            Property(e => e.PasswordHash).IsRequired().HasMaxLength(60);
            Ignore(e => e.Password);
        }
    }
}