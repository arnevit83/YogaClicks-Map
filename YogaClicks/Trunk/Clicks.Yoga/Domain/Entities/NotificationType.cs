namespace Clicks.Yoga.Domain.Entities
{
    public class NotificationType : Entity
    {
        public virtual NotificationCategory Category { get; set; }

        public string Tag { get; set; }

        public string Message { get; set; }

        public string Icon { get; set; }
    }
}