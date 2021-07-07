using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class TeacherTrainingCourseTeacherAddedHandler : IRequestCompletionHandler
    {
        public TeacherTrainingCourseTeacherAddedHandler(IEntityContext entityContext, INotificationService notificationService)
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

            NotificationService.Notify(association.Course.Owner, "TeacherTrainingCourseTeacherAccepted", association.Teacher.Owner.Profile, association.Course, association.Teacher);
        }

        public void Reject(Request request)
        {
            var association = GetAssociation(request);

            if (association == null) return;

            EntityContext.TeacherTrainingCourseTeachers.Remove(association);
        }

        private TeacherTrainingCourseTeacher GetAssociation(Request request)
        {
            return EntityContext.TeacherTrainingCourseTeachers.Get(request.EntityId);
        }
    }
}