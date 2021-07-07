using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Permissions.Providers
{
    public class ProfileWallPermissionProvider : IWallPermissionProvider
    {
        public ProfileWallPermissionProvider(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IFriendService friendService)
        {
            EntityContext = entityContext;
            SecurityContext = securityContext;
            FriendService = friendService;
        }

        private IEntityContext EntityContext { get; set; }
        
        private ISecurityContext SecurityContext { get; set; }

        private IFriendService FriendService { get; set; }

        public WallPermissions GetPermissions(Wall wall)
        {
            var permissions = new WallPermissions();

            if (!SecurityContext.Authenticated) return permissions;

            var specific = wall as ProfileWall;
            var updatable = SecurityContext.CanUpdate(specific.Profile);
            var status = FriendService.GetFriendshipStatus(specific.Profile.Id, SecurityContext.CurrentProfile.Id);
            var friends = status == FriendshipStatus.Friends;

            permissions.Access = updatable || friends || specific.Profile.Public;
            permissions.Post = updatable;
            permissions.Comment = updatable || friends;
            permissions.Administrate = updatable;

            return permissions;
        }
    }
}