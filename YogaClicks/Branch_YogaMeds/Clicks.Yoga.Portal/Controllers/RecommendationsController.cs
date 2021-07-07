using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Recommendations;
using Clicks.Yoga.Portal.Models.Shared;

namespace Clicks.Yoga.Portal.Controllers
{
    public class RecommendationsController : YogaController
    {
        public RecommendationsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService,
            INotificationService notificationService)
            : base(workUnit, securityContext)
        {
            EntityService = entityService;
            FriendService = friendService;
            NotificationService = notificationService;
        }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private INotificationService NotificationService { get; set; }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Button(int entityId, string entityTypeName)
        {
            if (SecurityContext.Authenticated && FriendService.GetFriendCount(SecurityContext.CurrentProfile.Id) == 0)
            {
                return new EmptyResult();
            }

            return View(new RecommendationButtonModel(entityId, entityTypeName));
        }

        
        [YogaAuthorize]
        public ActionResult Send(int entityId, string entityTypeName)
        {
            var entity = EntityService.GetEntity<IEntityHandle>(entityId, entityTypeName);
            var friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);

            return View(new RecommendationSendModel().PopulateMetadata(entity, friends));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Send(RecommendationSendModel model)
        {
            var entity = EntityService.GetEntity<IEntityHandle>(model.EntityId, model.EntityTypeName);

            IList<Profile> friends;

            if (model.Friends.Ids.Count == 0)
            {
                ModelState.AddModelError("", "Please select one or more friends.");

                friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);

                return View(model.PopulateMetadata(entity, friends));
            }

            if (model.Group == "All")
            {
                friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);
            }
            else
            {
                friends = new List<Profile>();

                foreach (var id in model.Friends.Ids)
                {
                    var friend = FriendService.GetFriend(SecurityContext.CurrentProfile.Id, id);

                    if (friend == null) continue;

                    friends.Add(friend);
                }
            }

            foreach (var friend in friends)
            {
                NotificationService.Notify(friend.Owner, "Recommendation", SecurityContext.CurrentActor, entity);
            }

            WorkUnit.Commit();

            var reason = "SendRecommendation";

            if (friends.Count > 1) reason += "s";

            return View("CloseModal", new CloseModalModel(null, reason));
        }
    }
}