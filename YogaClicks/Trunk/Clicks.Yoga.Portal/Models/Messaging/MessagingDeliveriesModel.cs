namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingDeliveriesModel
    {
        public MessagingDeliveriesModel(int profileId)
        {
            ProfileId = profileId;
        }

        public int ProfileId { get; private set; }
    }
}