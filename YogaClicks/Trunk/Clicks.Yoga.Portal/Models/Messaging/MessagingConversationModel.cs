using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingConversationModel
    {
        public MessagingConversationModel(PrivateConversation conversation, int? messageId, IPortalSecurityContext securityContext)
        {
            Conversation = new PrivateConversationModel(conversation);
            MessageId = messageId;

            var sender = securityContext.AvailableActors.FirstOrDefault(a =>
                conversation.Participants.Any(p => p.EntityId == a.Id && p.EntityType.Id == a.EntityType.Id));

            Sender = new EntityReference(sender.Id, sender.EntityType.Name);
        }

        public PrivateConversationModel Conversation { get; private set; }

        public int? MessageId { get; private set; }

        public EntityReference Sender { get; private set; }
    }
}