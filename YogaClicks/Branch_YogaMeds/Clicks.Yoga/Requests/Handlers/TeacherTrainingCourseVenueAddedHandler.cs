using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class TeacherTrainingCourseVenueAddedHandler : IRequestCompletionHandler
    {
        public TeacherTrainingCourseVenueAddedHandler(IEntityContext entityContext, INotificationService notificationService)
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

            NotificationService.Notify(association.Course.Owner, "TeacherTrainingCourseVenueAccepted", association.Venue.Owner.Profile, association.Course, association.Venue);
        }

        public void Reject(Request request)
        {
            var association = GetAssociation(request);

            if (association == null) return;

            EntityContext.TeacherTrainingCourseVenues.Remove(association);
        }

        private TeacherTrainingCourseVenue GetAssociation(Request request)
        {
            return EntityContext.TeacherTrainingCourseVenues.Get(request.EntityId);
        }
    }
}