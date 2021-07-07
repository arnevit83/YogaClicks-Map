using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IMessagingService
    {
        bool RecipientPermitted(EntityReference recipient);
        int GetUnreadCount();
        int GetUnreadCount(User user);
        PrivateConversation GetConversation(int id);
        IList<PrivateMessageDelivery> GetDeliveries(int profileId);
        IList<PrivateMessageDelivery> GetUnreadDeliveries(int profileId);
        IList<PrivateMessage> GetConversationMessages(int conversationId);
        IList<EntityHandle> GetConversationParticipants(int conversationId);
        PrivateConversation Send(IEnumerable<EntityReference> recipients, string content);
        PrivateMessage Reply(int conversationId, string content);
        void HideDeliveries(IEnumerable<int> ids);
        void ReadDeliveries(IEnumerable<int> ids);
        void ReadConversation(int id);
        void UnreadDeliveries(IEnumerable<int> ids);
    }
}