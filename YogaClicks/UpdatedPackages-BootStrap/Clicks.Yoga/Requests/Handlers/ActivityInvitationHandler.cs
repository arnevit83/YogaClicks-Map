using System;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class ActivityInvitationHandler : IRequestCompletionHandler
    {
        public ActivityInvitationHandler(
            IEntityContext entityContext,
            IEntityService entityService,
            INewsService newsService,
            INotificationService notificationService)
        {
            EntityContext = entityContext;
            EntityService = entityService;
            NewsService = newsService;
            NotificationService = notificationService;
        }

        private IEntityContext EntityContext { get; set; }

        private IEntityService EntityService { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        public void Accept(Request request)
        {
            var attendee = GetAttendee(request);

            if (attendee == null) return;

            attendee.Confirmed = true;

            var actor = EntityService.GetEntity<IActor>(attendee.Handle);

            NotificationService.Notify(attendee.Activity.Owner, "ActivityAttendeeConfirmed", actor, attendee.Activity);
            NewsService.PostAction(NewsStoryType.FriendAttendingActivity, attendee.Handle, attendee.Activity);
        }

        public void Reject(Request request)
        {
            var attendee = GetAttendee(request);
            
            if (attendee == null) return;

            attendee.Confirmed = false;
        }

        private ActivityAttendee GetAttendee(Request request)
        {
            return EntityContext.ActivityAttendees.Get(request.EntityId);
        }
    }
}