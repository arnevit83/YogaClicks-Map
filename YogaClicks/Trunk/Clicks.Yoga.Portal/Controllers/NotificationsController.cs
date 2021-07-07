using System;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Notifications;

namespace Clicks.Yoga.Portal.Controllers
{
    public class NotificationsController : YogaController
    {
        public NotificationsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            INotificationService notificationService,
            IRequestService requestService,
            IFriendService friendService)
            : base(workUnit, securityContext)
        {
            NotificationService = notificationService;
            RequestService = requestService;
            FriendService = friendService;
        }
        
        private INotificationService NotificationService { get; set; }

        private IRequestService RequestService { get; set; }

        private IFriendService FriendService { get; set; }

        [HttpPost]
        public ActionResult Count()
        {
            if (!SecurityContext.Authenticated) return new EmptyResult();

            var count = NotificationService.GetUnreadNotificationCount(SecurityContext.CurrentUser);
            count += RequestService.GetPendingRequestCount(SecurityContext.CurrentUser);

            return Json(count);
        }

        [HttpPost]
        public ActionResult FriendRequestCount()
        {
            if (!SecurityContext.Authenticated) return new EmptyResult();

            var count = RequestService.GetPendingFriendRequestCount(SecurityContext.CurrentUser);

            return Json(count);
        }

        public ActionResult PopupPartial()
        {
            if (!SecurityContext.Authenticated) return new EmptyResult();

            var deliveries = NotificationService.GetUnreadNotifications(SecurityContext.CurrentUser);
            var requests = RequestService.GetPendingRequests(SecurityContext.CurrentUser);

            var model = new NotificationsPopupPartialModel(deliveries, requests);

            NotificationService.Dismiss(deliveries);
            WorkUnit.Commit();

            return View(model);
        }

        public ActionResult FriendRequestsPopupPartial()
        {
            if (!SecurityContext.Authenticated) return new EmptyResult();

            var requests = RequestService.GetPendingFriendRequests(SecurityContext.CurrentUser);

            return View(new FriendRequestsPopupPartialModel(requests));
        }

        public ActionResult SubjectRequestsPartial(int subjectId, string subjectTypeName)
        {
            if (!SecurityContext.Authenticated) return new EmptyResult();

            var requests = RequestService.GetPendingRequestsBySubject(subjectId, subjectTypeName, SecurityContext.CurrentUser);

            return View(new SubjectRequestsPartialModel(requests));
        }

        public ActionResult Accept(int requestId)
        {
            RequestService.Accept(requestId);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        public ActionResult Reject(int requestId)
        {
            RequestService.Reject(requestId);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        public ActionResult Block(int requestId)
        {
            var request = RequestService.GetPendingRequest(requestId);

            RequestService.Reject(request);
            FriendService.Block(request.User.Profile.Id, request.EntityId);

            WorkUnit.Commit();

            return new EmptyResult();
        }
    }
}
