using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NotificationDeliveryMapping : EntityMapping<NotificationDelivery>
    {
        public NotificationDeliveryMapping()
        {
            HasRequired(e => e.Notification).WithMany(e => e.Deliveries).Map(a => a.MapKey("Notificationid"));
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            Property(e => e.Read).IsRequired();
        }
    }
}