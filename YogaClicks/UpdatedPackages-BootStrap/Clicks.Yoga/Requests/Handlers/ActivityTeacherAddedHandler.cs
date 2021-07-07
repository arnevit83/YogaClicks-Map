using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class ActivityTeacherAddedHandler : IRequestCompletionHandler
    {
        public ActivityTeacherAddedHandler(IEntityContext entityContext, INotificationService notificationService)
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

            NotificationService.Notify(association.Activity.Owner, "ActivityTeacherAccepted", association.Teacher, association.Activity);
        }

        public void Reject(Request request)
        {
            var association = GetAssociation(request);

            if (association == null) return;

            NotificationService.Notify(association.Activity.Owner, "ActivityTeacherRejected", association.Teacher, association.Activity);

            EntityContext.ActivityTeachers.Remove(association);
        }

        private ActivityTeacher GetAssociation(Request request)
        {
            return EntityContext.ActivityTeachers.Get(request.EntityId);
        }
    }
}