using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NotificationCategoryMapping : EntityMapping<NotificationCategory>
    {
        public NotificationCategoryMapping()
        {
            Property(e => e.Tag).IsRequired();
            Property(e => e.Name).IsRequired();
        }
    }
}