using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class MessagingService : ServiceBase, IMessagingService
    {
        public MessagingService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            FriendService = friendService;
        }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        public bool RecipientPermitted(EntityReference recipient)
        {
            if (!SecurityContext.HasActor) return false;

            if (recipient.EntityTypeName == typeof(Profile).Name)
            {
                if(SecurityContext.IsSuperUser())
                    return true;

                return FriendService.GetFriendshipStatus(SecurityContext.CurrentProfile.Id, recipient.EntityId) == FriendshipStatus.Friends;    
            }
            else
            {
                var entity = EntityService.GetEntity<PrincipalEntity>(recipient);
                return entity != null && entity.Owner != null && entity.Owner.Id != SecurityContext.CurrentUser.Id;
            }
        }

        public int GetUnreadCount()
        {
            if (!SecurityContext.Authenticated) return 0;

            return EntityContext.PrivateMessageDeliveries
                .Where(d => d.Recipient.Owner.Id == SecurityContext.CurrentUser.Id)
                .Where(d => !d.Hidden)
                .Count(d => !d.Read);
        }

        public int GetUnreadCount(User user)
        {
            if (user == null)
                throw new ArgumentNullException();
            if (!SecurityContext.CanUpdate(user))
                throw new PermissionDeniedException();

            return EntityContext.PrivateMessageDeliveries
                .Where(d => d.Recipient.Owner.Id == user.Id)
                .Where(d => !d.Hidden)
                .Count(d => !d.Read);
        }

        public PrivateConversation GetConversation(int id)
        {
            return EntityContext.PrivateConversations.Get(id);
        }

        public IList<PrivateMessageDelivery> GetDeliveries(int profileId)
        {
            var profile = EntityContext.Profiles.GetIgnoringActive(profileId);

            if (profile == null)
                throw new ArgumentOutOfRangeException("profileId");
            if (!SecurityContext.CanUpdate(profile))
                throw new PermissionDeniedException();

            return EntityContext.PrivateMessageDeliveries
                .Include(d => d.Message)
                .Include(d => d.Message.Sender)
                .Include(d => d.Message.Sender.Image)
                .Where(d => d.Recipient.Owner.Id == profile.Owner.Id)
                .Where(d => !d.Hidden)
                .OrderByDescending(d => d.CreationTime)
                .ToList();
        }

        public IList<PrivateMessageDelivery> GetUnreadDeliveries(int profileId)
        {
            var profile = EntityContext.Profiles.GetIgnoringActive(profileId);

            if (profile == null)
                throw new ArgumentOutOfRangeException("profileId");
            if (!SecurityContext.CanUpdate(profile))
                throw new PermissionDeniedException();

            return EntityContext.PrivateMessageDeliveries
                .Include(d => d.Message)
                .Include(d => d.Message.Sender)
                .Include(d => d.Message.Sender.Image)
                .Where(d => d.Recipient.Owner.Id == profile.Owner.Id)
                .Where(d => !d.Hidden)
                .Where(d => !d.Read)
                .OrderByDescending(d => d.CreationTime)
                .ToList();
        }

        public IList<PrivateMessage> GetConversationMessages(int conversationId)
        {
            if (!SecurityContext.Authenticated)
                throw new PermissionDeniedException();

            if (!SecurityContext.IsSuperUser())
            {
                var conversation = EntityContext.PrivateConversations.Get(conversationId);

                if (!conversation.Participants.Any(p => p.Owner.Id == SecurityContext.CurrentUser.Id))
                    throw new PermissionDeniedException();
            }

            return EntityContext.PrivateMessages
                .Include(m => m.Sender)
                .Include(m => m.Sender.Image)
                .Where(m => m.Conversation.Id == conversationId)
                .OrderBy(m => m.CreationTime)
                .ToList();
        }

        public IList<EntityHandle> GetConversationParticipants(int conversationId)
        {
            if (!SecurityContext.Authenticated)
                throw new PermissionDeniedException();
            
            var conversation = EntityContext.PrivateConversations.Get(conversationId);

            if (!SecurityContext.IsSuperUser())
            {
                if (!conversation.Participants.Any(p => p.Owner.Id == SecurityContext.CurrentUser.Id))
                    throw new PermissionDeniedException();
            }

            return conversation.Participants.ToList();
        }

        public PrivateConversation Send(IEnumerable<EntityReference> recipients, string content)
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var handles = new List<EntityHandle>();

            foreach (var recipient in recipients)
            {
                var handle = EntityService.EnsureEntityHandle(recipient.EntityId, recipient.EntityTypeName);

                if (handle == null)
                    throw new ArgumentOutOfRangeException("recipients");

                if (!SecurityContext.IsSuperUser() && recipient.EntityTypeName == typeof(Profile).Name)
                {
                    var status = FriendService.GetFriendshipStatus(SecurityContext.CurrentProfile.Id, recipient.EntityId);

                    if (status != FriendshipStatus.Friends)
                        throw new PermissionDeniedException();
                }

                handles.Add(handle);
            }

            var senderHandle = EntityService.EnsureEntityHandle(SecurityContext.CurrentActor);

            var conversation = CreateConversation(senderHandle, handles);

            CreateMessage(conversation, senderHandle, content);

            return conversation;
        }

        public PrivateMessage Reply(int conversationId, string content)
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var conversation = EntityContext.PrivateConversations.Get(conversationId);

            if (conversation == null)
                throw new ArgumentOutOfRangeException("conversationId");

            var senderHandle = EntityService.EnsureEntityHandle(SecurityContext.CurrentActor);

            var message = CreateMessage(conversation, senderHandle, content);

            return message;
        }

        public void HideDeliveries(IEnumerable<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            foreach (var id in ids)
            {
                var delivery = EntityContext.PrivateMessageDeliveries.Get(id);

                if (delivery == null) continue;

                if (!SecurityContext.CanUpdate(delivery.Recipient))
                    throw new PermissionDeniedException();

                delivery.Hidden = true;
            }
        }

        public void ReadDeliveries(IEnumerable<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            foreach (var id in ids)
            {
                var delivery = EntityContext.PrivateMessageDeliveries.Get(id);

                if (delivery == null) continue;

                if (!SecurityContext.CanUpdate(delivery.Recipient))
                    throw new PermissionDeniedException();

                delivery.Read = true;
                delivery.ReadTime = DateTime.Now;
            }
        }

        public void ReadConversation(int id)
        {
            var deliveries = EntityContext.PrivateMessageDeliveries
                .Where(d => d.Message.Conversation.Id == id)
                .Where(d => d.Recipient.Owner.Id == SecurityContext.CurrentUser.Id)
                .Where(d => !d.Read);

            foreach (var delivery in deliveries)
            {
                delivery.Read = true;
                delivery.ReadTime = DateTime.Now;
            }
        }

        public void UnreadDeliveries(IEnumerable<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            foreach (var id in ids)
            {
                var delivery = EntityContext.PrivateMessageDeliveries.Get(id);

                if (delivery == null) continue;

                if (!SecurityContext.CanUpdate(delivery.Recipient))
                    throw new PermissionDeniedException();

                delivery.Read = false;
                delivery.ReadTime = null;
            }
        }

        private PrivateConversation CreateConversation(EntityHandle sender, IEnumerable<EntityHandle> recipients)
        {
            var conversation = new PrivateConversation();
            
            conversation.Participants.Add(sender);

            foreach (var recipient in recipients)
            {
                conversation.Participants.Add(recipient);
            }

            EntityContext.PrivateConversations.Add(conversation);

            return conversation;
        }

        private PrivateMessage CreateMessage(PrivateConversation conversation, EntityHandle sender, string content)
        {
            if (!conversation.Participants.Any(p => p.Id == sender.Id))
            {
                conversation.Participants.Add(sender);
            }

            var message = new PrivateMessage
            {
                Conversation = conversation,
                Sender = sender,
                Content = content
            };

            conversation.Messages.Add(message);

            foreach (var participant in conversation.Participants)
            {
                if (participant.Id == sender.Id) continue;

                message.Deliveries.Add(new PrivateMessageDelivery { Message = message, Recipient = participant });

            }

            return message;
        }
    }
}