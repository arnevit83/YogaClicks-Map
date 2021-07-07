using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Emails;
using Clicks.Yoga.Portal.Models.EmailNotifications;

namespace Clicks.Yoga.Portal.Controllers
{
    public class EmailNotificationsController : YogaApiController
    {
        public EmailNotificationsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IAccountService accountService,
            INotificationService notificationService,
            IRequestService requestService,
            IProfileService profileService,
            IMessagingService messagingService)
            : base(workUnit, securityContext)
        {
            AccountService = accountService;
            NotificationService = notificationService;
            RequestService = requestService;
            ProfileService = profileService;
            MessagingService = messagingService;
        }

        private IAccountService AccountService { get; set; }

        private INotificationService NotificationService { get; set; }

        private IRequestService RequestService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IMessagingService MessagingService { get; set; }

        [HttpGet]
        public IList<int> OverdueUserIds()
        {
            var users = NotificationService.GetUsersDueDigest();
            return users.Select(e => e.Id).ToList();
        }

        [HttpPost]
        public void SendDigestEmail(SendDigestEmailRequest request)
        {
            var user = AccountService.GetUser(request.UserId);

            if (user == null)
            {
                return;
            }

            SecurityContext.SetCurrentUser(user, false);

            NotificationService.EnsureNotificationPreferencesExist(user);

            if (!user.NotificationPreferences.DigestEmailDue)
            {
                return;
            }

            user.NotificationPreferences.CalculateNextDigestEmailTime();
            WorkUnit.Commit();

            var notifications = NotificationService.GetUnreadNotifications(user);
            var requests = RequestService.GetPendingRequests(user);

            var categories = NotificationService.GetSelectedDigestNotificationCategories(user);
            var messageCount = MessagingService.GetUnreadCount(user);
            var notificationCounts = new Dictionary<NotificationCategory, int>();
            var requestCounts = new Dictionary<NotificationCategory, int>();

            foreach (var item in categories)
            {
                var category = item;
                var categoryNotifications = notifications.Where(n => n.Notification.Type.Category.Id == category.Id).ToList();
                var categoryRequests = requests.Where(r => r.Type.Category.Id == category.Id).ToList();

                if (categoryNotifications.Count > 0)
                {
                    notificationCounts.Add(category, categoryNotifications.Count);
                }

                if (categoryRequests.Count > 0)
                {
                    requestCounts.Add(category, categoryRequests.Count);
                }
            }

            var friendshipCategory = categories.FirstOrDefault(c => c.IsFriendshipRequests);

            if (friendshipCategory != null)
            {
                var friendshipRequests = RequestService.GetPendingFriendRequests(user).ToList();
                requestCounts[friendshipCategory] = friendshipRequests.Count;
            }

            var email = new NotificationDigestEmail
            {
                MessageCount = messageCount,
                Notifications = notificationCounts,
                Requests = requestCounts,
                Profile = user.Profile
            };

            if (email.AnyNotifications)
            {
                email.Send();
            }
        }
    }
}
