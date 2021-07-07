using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions.Providers
{
    public class ActivityWallPermissionProvider : IWallPermissionProvider
    {
        public ActivityWallPermissionProvider(IEntityContext entityContext, ISecurityContext securityContext)
        {
            EntityContext = entityContext;
            SecurityContext = securityContext;
        }

        private IEntityContext EntityContext { get; set; }
        
        private ISecurityContext SecurityContext { get; set; }

        public WallPermissions GetPermissions(Wall wall)
        {
            var permissions = new WallPermissions();

            if (!SecurityContext.Authenticated) return permissions;

            var specific = wall as ActivityWall;
            var activity = specific.Activity;
            var user = SecurityContext.CurrentUser;
            var updatable = SecurityContext.CanUpdate(activity);
            var attendee = EntityContext.ActivityAttendees.FirstOrDefault(e =>
                e.Activity.Id == activity.Id &&
                e.Handle.Owner.Id == user.Id &&
                e.Confirmed.HasValue &&
                e.Confirmed.Value);
            var attending = attendee != null;

            permissions.Access = updatable || attending || activity.Public;
            permissions.Post = updatable || (attending && activity.AttendeePostingPermitted);
            permissions.Comment = updatable || (attending && activity.AttendeePostingPermitted);
            permissions.Administrate = updatable;

            return permissions;
        }
    }
}