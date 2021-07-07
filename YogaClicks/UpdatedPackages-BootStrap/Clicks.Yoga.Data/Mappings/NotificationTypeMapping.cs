using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NotificationTypeMapping : EntityMapping<NotificationType>
    {
        public NotificationTypeMapping()
        {
            HasOptional(e => e.Category).WithMany(e => e.NotificationTypes).Map(a => a.MapKey("CategoryId"));
            Property(e => e.Tag).IsRequired();
            Property(e => e.Message).IsRequired();
            Property(e => e.Icon).IsRequired();
        }
    }
}