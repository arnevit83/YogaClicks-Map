using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface ICommentPermissionProviderFactory
    {
        ICommentPermissionProvider GetProvider(ICommentable target);
    }
}