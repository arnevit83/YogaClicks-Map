using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Messaging;

namespace Clicks.Yoga.Portal.Controllers
{
    public class MessagingController : YogaController
    {
        public MessagingController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService,
            IMessagingService messagingService)
            : base(workUnit, securityContext)
        {
            EntityService = entityService;
            FriendService = friendService;
            MessagingService = messagingService;
        }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private IMessagingService MessagingService { get; set; }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult PopupPartial()
        {
            var deliveries = SecurityContext.Authenticated ? MessagingService.GetUnreadDeliveries(SecurityContext.CurrentProfile.Id) : new List<PrivateMessageDelivery>();
            return View(new MessagingPopupPartialModel(deliveries));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult UnreadCount()
        {
            return Json(MessagingService.GetUnreadCount());
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult SendButton(EntityReference recipient, string text)
        {
            var permitted = MessagingService.RecipientPermitted(recipient);
            return PartialView(new MessagingSendButtonModel(recipient, text, permitted));
        }

        
        [YogaAuthorize]
        public ActionResult Deliveries(int id)
        {
            return View(new MessagingDeliveriesModel(id));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [YogaAuthorize]
        public ActionResult DeliveriesPartial(int id)
        {
            var deliveries = MessagingService.GetDeliveries(id);
            return View("DeliveriesPartial", new MessagingDeliveriesPartialModel(deliveries));
        }

        
        [YogaAuthorize]
        public ActionResult Conversation(int conversationId, int? messageId)
        {
            var conversation = MessagingService.GetConversation(conversationId);
            return View(new MessagingConversationModel(conversation, messageId, (IPortalSecurityContext)SecurityContext));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [YogaAuthorize]
        public ActionResult ConversationPartial(int conversationId)
        {
            var messages = MessagingService.GetConversationMessages(conversationId);
            var participants = MessagingService.GetConversationParticipants(conversationId);
            var sender = participants.First(p => SecurityContext.IsOwner(p));
            var recipient = participants.First(p => !SecurityContext.IsOwner(p));

            MessagingService.ReadConversation(conversationId);
            WorkUnit.Commit();

            return View("ConversationPartial", new MessagingConversationPartialModel(messages, sender, recipient));
        }

        
        [YogaAuthorize]
        public ActionResult Send(EntityReference recipient)
        {
            var friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);
            var model = new MessagingSendModel().PopuplateMetadata(friends);

            if (recipient.EntityId > 0)
            {
                if (SecurityContext.IsSuperUser())
                {
                    var handle = EntityService.EnsureEntityHandle(recipient);
                    model.Recipient = new EntityHandleModel(handle);
                }

                if (recipient.EntityTypeName == null || recipient.EntityTypeName == typeof(Profile).Name)
                {
                    model.Friends.Selection[recipient.EntityId] = true;
                }
                else
                {
                    var handle = EntityService.EnsureEntityHandle(recipient);
                    model.Recipient = new EntityHandleModel(handle);
                    WorkUnit.Commit();
                }
            }

            return View(model);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Send(MessagingSendModel model)
        {
            if (!ModelState.IsValid)
            {
                var friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);
                return View(model.PopuplateMetadata(friends));
            }

            var conversation = MessagingService.Send(model.CombinedRecipients, model.Content);
            WorkUnit.Commit();

            return RedirectToAction("Conversation", new { ConversationId = conversation.Id });
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Reply(int conversationId, string content)
        {
            MessagingService.Reply(conversationId, content);
            WorkUnit.Commit();

            return ConversationPartial(conversationId);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult HideDeliveries(int id, int[] deliveryIds)
        {
            if (deliveryIds != null)
            {
                MessagingService.HideDeliveries(deliveryIds);
                WorkUnit.Commit();
            }

            return DeliveriesPartial(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ReadDeliveries(int id, int[] deliveryIds)
        {
            if (deliveryIds != null)
            {
                MessagingService.ReadDeliveries(deliveryIds);
                WorkUnit.Commit();
            }

            return DeliveriesPartial(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult UnreadDeliveries(int id, int[] deliveryIds)
        {
            if (deliveryIds != null)
            {
                MessagingService.UnreadDeliveries(deliveryIds);
                WorkUnit.Commit();
            }
            
            return DeliveriesPartial(id);
        }
    }
}