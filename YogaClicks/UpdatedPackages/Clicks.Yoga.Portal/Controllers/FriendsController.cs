using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Friends;

namespace Clicks.Yoga.Portal.Controllers
{
    public class FriendsController : YogaController
    {
        public FriendsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IFriendService friendService)
            : base(workUnit, securityContext)
        {
            FriendService = friendService;
        }

        private IFriendService FriendService { get; set; }

        public ActionResult Button(int profileId, string profileName, FriendshipStatus? status)
        {
            if (status == null)
            {
                if (SecurityContext.Authenticated)
                {
                    status = FriendService.GetFriendshipStatus(SecurityContext.CurrentUser.Profile.Id, profileId);
                }
                else
                {
                    status = FriendshipStatus.Unknown;
                }
            }

            return View(new FriendButtonModel(profileId, profileName, status.Value));
        }

        public ActionResult Befriend(int profileId, string profileName)
        {
            if (!SecurityContext.HasActor) return new EmptyResult();

            FriendService.Request(SecurityContext.CurrentUser.Profile.Id, profileId);
            WorkUnit.Commit();

            return View("Button", new FriendButtonModel(profileId, profileName, FriendshipStatus.Pending));
        }
    }
}