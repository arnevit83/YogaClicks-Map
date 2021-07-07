using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Walls
{
    public class WallPostPartialModel
    {
        public WallPostPartialModel(WallPost post, WallPermissions permissions)
        {
            Post = new WallPostModel(post);
            Permissions = permissions;
        }

        public WallPostPartialModel(WallPostModel post, WallPermissions permissions)
        {
            Post = post;
            Permissions = permissions;
        }

        public WallPostModel Post { get; private set; }

        public WallPermissions Permissions { get; private set; }

        public CommentPermissions CommentPermissions
        {
            get
            {
                return new CommentPermissions
                {
                    Create = Permissions.Comment,
                    Delete = Permissions.Administrate
                };
            }
        }
    }
}