using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingSendButtonModel
    {
        public MessagingSendButtonModel(EntityReference recipient, string text, bool permitted)
        {
            Recipient = recipient;
            Text = text;
            Permitted = permitted;
        }

        public EntityReference Recipient { get; private set; }

        public string Text { get; private set; }

        public bool Permitted { get; private set; }
    }
}