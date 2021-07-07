using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface INewsPermissionProvider
    {
        NewsPermissions GetPermissions(Wall wall);
    }
}