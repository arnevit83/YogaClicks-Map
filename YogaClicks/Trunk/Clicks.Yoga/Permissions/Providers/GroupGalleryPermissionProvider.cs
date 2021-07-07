using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Permissions.Providers
{
    public class GroupGalleryPermissionProvider : IGalleryPermissionProvider
    {
        public GroupGalleryPermissionProvider(ISecurityContext securityContext, IGroupService groupService)
        {
            SecurityContext = securityContext;
            GroupService = groupService;
        }

        private ISecurityContext SecurityContext { get; set; }

        private IGroupService GroupService { get; set; }

        public GalleryPermissions GetPermissions(IGalleryAssociate associate)
        {
            var group = associate as Group;
            var updatable = SecurityContext.CanUpdate(group);
            var status = GroupService.GetMembershipStatus(group.Id);

            return new GalleryPermissions
            {
                Associate = updatable || (group.MemberGalleriesPermitted && status == GroupMembershipStatus.Confirmed),
                Disassociate = updatable
            };
        }
    }
}