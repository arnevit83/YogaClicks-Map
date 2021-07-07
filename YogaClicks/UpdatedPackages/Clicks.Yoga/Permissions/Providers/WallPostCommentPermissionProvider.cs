using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions.Providers
{
    public class WallPostCommentPermissionProvider : ICommentPermissionProvider
    {
        public WallPostCommentPermissionProvider(IWallPermissionProviderFactory wallPermissionProviderFactory)
        {
            WallPermissionProviderFactory = wallPermissionProviderFactory;
        }

        private IWallPermissionProviderFactory WallPermissionProviderFactory { get; set; }

        public CommentPermissions GetPermissions(ICommentable target)
        {
            var post = target as WallPost;

            if (post == null)
                throw new ArgumentException("Target must be an instance of WallPost.");

            var provider = WallPermissionProviderFactory.GetProvider(post.Wall);
            var permissions = provider.GetPermissions(post.Wall);

            return new CommentPermissions
            {
                Create = permissions.Comment,
                Delete = permissions.Administrate
            };
        }
    }
}