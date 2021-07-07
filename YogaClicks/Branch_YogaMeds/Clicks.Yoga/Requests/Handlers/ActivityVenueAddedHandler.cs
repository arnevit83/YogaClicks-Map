using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class ActivityVenueAddedHandler : IRequestCompletionHandler
    {
        public ActivityVenueAddedHandler(IEntityContext entityContext, INotificationService notificationService)
        {
            EntityContext = entityContext;
            NotificationService = notificationService;
        }

        private IEntityContext EntityContext { get; set; }

        private INotificationService NotificationService { get; set; }

        public void Accept(Request request)
        {
            var activity = GetActivity(request);

            if (activity == null) return;

            NotificationService.Notify(activity.Owner, "ActivityVenueAccepted", activity.Venue.Owner.Profile, activity, activity.Venue);
        }

        public void Reject(Request request)
        {
            var activity = GetActivity(request);

            if (activity == null) return;

            var venue = activity.Venue;

            if (venue == null) return;

            activity.Venue = null;

            NotificationService.Notify(activity.Owner, "ActivityVenueRejected", venue.Owner.Profile, activity, venue);
        }

        private Activity GetActivity(Request request)
        {
            return EntityContext.Activities.Get(request.EntityId);
        }
    }
}