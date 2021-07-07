using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface IWallPermissionProvider
    {
        WallPermissions GetPermissions(Wall wall);
    }
}