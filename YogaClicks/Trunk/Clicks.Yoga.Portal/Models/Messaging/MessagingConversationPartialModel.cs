using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingConversationPartialModel
    {
        public MessagingConversationPartialModel(IEnumerable<PrivateMessage> messages, EntityHandle sender, EntityHandle recipient)
        {
            Messages = new List<PrivateMessageModel>(messages.Select(m => new PrivateMessageModel(m, sender, recipient)));
            Recipient = new EntityHandleModel(recipient);
        }

        public IList<PrivateMessageModel> Messages { get; private set; }

        public EntityHandleModel Recipient { get; private set; }
    }
}