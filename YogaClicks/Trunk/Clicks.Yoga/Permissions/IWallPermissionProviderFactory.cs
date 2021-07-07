using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface IWallPermissionProviderFactory
    {
        IWallPermissionProvider GetProvider(Wall wall);
    }
}