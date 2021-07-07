using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NotificationCategoryPreferencesMapping : EntityMapping<NotificationCategoryPreferences>
    {
        public NotificationCategoryPreferencesMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            HasRequired(e => e.Category).WithMany().Map(a => a.MapKey("NotificationCategoryId"));
            Property(e => e.NotificationEnabled).IsRequired();
        }
    }
}