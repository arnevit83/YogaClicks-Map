using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface IGalleryPermissionProviderFactory
    {
        IGalleryPermissionProvider GetProvider(IGalleryAssociate associate);
    }
}