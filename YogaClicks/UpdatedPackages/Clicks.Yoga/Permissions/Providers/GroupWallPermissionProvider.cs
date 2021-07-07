using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions.Providers
{
    public class GroupWallPermissionProvider : IWallPermissionProvider
    {
        public GroupWallPermissionProvider(IEntityContext entityContext, ISecurityContext securityContext)
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

            var specific = wall as GroupWall;
            var group = specific.Group;
            var user = SecurityContext.CurrentUser;
            var member = EntityContext.GroupMembers.FirstOrDefault(e => e.Group.Id == group.Id && e.User.Id == user.Id && e.Confirmed);
            var isMember = member != null;
            var isAdministrator = isMember && member.Administrator;

            permissions.Access = group.Public || isMember;
            permissions.Post = isAdministrator || (isMember && group.MemberPostingPermitted);
            permissions.Comment = isAdministrator || (isMember && group.MemberPostingPermitted);
            permissions.Administrate = isAdministrator;

            return permissions;
        }
    }
}