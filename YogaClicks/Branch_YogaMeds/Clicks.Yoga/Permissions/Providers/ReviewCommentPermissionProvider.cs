using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions.Providers
{
    public class ReviewCommentPermissionProvider : ICommentPermissionProvider
    {
        public ReviewCommentPermissionProvider(ISecurityContext securityContext)
        {
            SecurityContext = securityContext;
        }

        private ISecurityContext SecurityContext { get; set; }

        public CommentPermissions GetPermissions(ICommentable target)
        {
            return new CommentPermissions
            {
                Create = SecurityContext.Authenticated,
                Delete = SecurityContext.Authenticated && SecurityContext.CurrentUser.IsSuperUser
            };
        }
    }
}