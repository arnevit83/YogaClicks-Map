using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface ICommentPermissionProvider
    {
        CommentPermissions GetPermissions(ICommentable target);
    }
}
