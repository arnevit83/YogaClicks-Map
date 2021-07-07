using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class VenuePlacementAddedHandler : IRequestCompletionHandler
    {
        public VenuePlacementAddedHandler(IEntityContext entityContext, INotificationService notificationService)
        {
            EntityContext = entityContext;
            NotificationService = notificationService;
        }

        private IEntityContext EntityContext { get; set; }

        private INotificationService NotificationService { get; set; }
        
        public void Accept(Request request)
        {
            var placement = GetPlacement(request);

            if (placement == null) return;

            placement.Confirmed = true;

            NotificationService.Notify(placement.Venue.Owner, "VenuePlacementAccepted", placement.Teacher.Owner.Profile, placement.Teacher, placement.Venue);
        }

        public void Reject(Request request)
        {
            var placement = GetPlacement(request);

            if (placement == null) return;

            EntityContext.TeacherPlacements.Remove(placement);
        }

        private TeacherPlacement GetPlacement(Request request)
        {
            return EntityContext.TeacherPlacements.Get(request.EntityId);
        }
    }
}