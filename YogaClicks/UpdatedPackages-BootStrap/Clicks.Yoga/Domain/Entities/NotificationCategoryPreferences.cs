namespace Clicks.Yoga.Domain.Entities
{
    public class NotificationCategoryPreferences : Entity
    {
        public virtual User User { get; set; }

        public virtual NotificationCategory Category { get; set; }

        public bool NotificationEnabled { get; set; }
    }
}