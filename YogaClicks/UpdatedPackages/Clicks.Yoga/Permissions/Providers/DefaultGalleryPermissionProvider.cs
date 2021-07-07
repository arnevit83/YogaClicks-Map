using System;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Permissions.Providers
{
    public class DefaultGalleryPermissionProvider : IGalleryPermissionProvider
    {
        public DefaultGalleryPermissionProvider(ISecurityContext securityContext)
        {
            SecurityContext = securityContext;
        }

        private ISecurityContext SecurityContext { get; set; }

        public GalleryPermissions GetPermissions(IGalleryAssociate associate)
        {
            var entity = associate as PrincipalEntity;
            var updatable = SecurityContext.CanUpdate(entity);

            return new GalleryPermissions
            {
                Associate = updatable,
                Disassociate = updatable
            };
        }
    }
}