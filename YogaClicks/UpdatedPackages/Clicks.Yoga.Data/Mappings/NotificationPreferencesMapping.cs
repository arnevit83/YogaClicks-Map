using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NotificationPreferencesMapping : EntityMapping<NotificationPreferences>
    {
        public NotificationPreferencesMapping()
        {
            HasRequired(e => e.User).WithOptional(e => e.NotificationPreferences).Map(a => a.MapKey("UserId"));
            Property(e => e.EmailDigestEnabled).IsRequired();
            HasRequired(e => e.EmailDigestTimescale).WithMany().Map(a => a.MapKey("EmailDigestTimescaleId"));
        }
    }
}