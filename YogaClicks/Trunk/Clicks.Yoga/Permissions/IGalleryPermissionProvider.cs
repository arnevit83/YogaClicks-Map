using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions
{
    public interface IGalleryPermissionProvider
    {
        GalleryPermissions GetPermissions(IGalleryAssociate associate);
    }
}