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
            var association = GetAssociation(request);

            if (association == null) return;

            association.Confirmed = true;

            NotificationService.Notify(association.Activity.Owner, "ActivityVenueAccepted", association.Venue, association.Activity, association.Venue);
        }

        public void Reject(Request request)
        {
            var association = GetAssociation(request);

            if (association == null) return;

            NotificationService.Notify(association.Activity.Owner, "ActivityVenueRejected", association.Venue, association.Activity, association.Venue);

            // Need to find out if lucy would like the whole activity deleted as its not possible to have an activity without a venue.
            //EntityContext.ActivityVenue.Remove(association);
        }

        private ActivityVenue GetAssociation(Request request)
        {
            return EntityContext.ActivityVenue.Get(request.EntityId);
        }
    }
}