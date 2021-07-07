using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class UserMapping : EntityMapping<User>
    {
        public UserMapping()
        {
            HasMany(e => e.Roles).WithRequired(e => e.User);
            Property(e => e.EmailAddress).IsRequired().HasMaxLength(254);
            Property(e => e.Active).IsRequired();
            HasOptional(e => e.PrivacyPreferences).WithMany().Map(a => a.MapKey("PrivacyPreferencesId"));
        }
    }
}