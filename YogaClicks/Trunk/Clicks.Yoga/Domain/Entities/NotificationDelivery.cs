namespace Clicks.Yoga.Domain.Entities
{
    public class NotificationDelivery : Entity
    {
        public NotificationDelivery() {}

        public NotificationDelivery(Notification notification, User user, bool read)
        {
            Notification = notification;
            User = user;
            Read = read;
        }

        public virtual Notification Notification { get; set; }

        public virtual User User { get; set; }

        public bool Read { get; set; }
    }
}