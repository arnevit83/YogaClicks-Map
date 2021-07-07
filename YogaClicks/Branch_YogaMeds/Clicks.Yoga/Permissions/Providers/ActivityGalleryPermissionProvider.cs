using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Permissions.Providers
{
    public class ActivityGalleryPermissionProvider : IGalleryPermissionProvider
    {
        public ActivityGalleryPermissionProvider(ISecurityContext securityContext, IActivityService activityService)
        {
            SecurityContext = securityContext;
            ActivityService = activityService;
        }

        private ISecurityContext SecurityContext { get; set; }

        private IActivityService ActivityService { get; set; }

        public GalleryPermissions GetPermissions(IGalleryAssociate associate)
        {
            var activity = associate as Activity;
            var updatable = SecurityContext.CanUpdate(activity);
            var status = SecurityContext.Authenticated ?
                ActivityService.GetAttendanceStatus(activity.Id, SecurityContext.CurrentActor) :
                ActivityAttendanceStatus.Closed;

            return new GalleryPermissions
            {
                Associate = updatable || (activity.AttendeeGalleriesPermitted && status == ActivityAttendanceStatus.Confirmed),
                Disassociate = updatable
            };
        }
    }
}