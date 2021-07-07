using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface INewsPermissionProviderFactory
    {
        INewsPermissionProvider GetProvider(Wall wall);
    }
}