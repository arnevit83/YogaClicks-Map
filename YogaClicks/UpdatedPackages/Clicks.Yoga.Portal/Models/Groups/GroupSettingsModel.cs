using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupSettingsModel
    {
        public GroupSettingsModel() {}

        public GroupSettingsModel(Group group, GroupPermissions permissions)
        {
            Group = new GroupModel(group);
            Permissions = permissions;
            Public = group.Public;
            MemberInvitationsPermitted = group.MemberInvitationsPermitted;
            MemberPostingPermitted = group.MemberPostingPermitted;
            MemberGalleriesPermitted = group.MemberGalleriesPermitted;
        }

        public GroupModel Group { get; private set; }

        public GroupPermissions Permissions { get; private set; }

        public bool Public { get; set; }

        public bool MemberInvitationsPermitted { get; set; }

        public bool MemberPostingPermitted { get; set; }

        public bool MemberGalleriesPermitted { get; set; }

        public void PopulateEntity(Group group)
        {
            group.Public = Public;
            group.MemberInvitationsPermitted = MemberInvitationsPermitted;
            group.MemberPostingPermitted = MemberPostingPermitted;
            group.MemberGalleriesPermitted = MemberGalleriesPermitted;
        }

        public GroupSettingsModel PopuplateMetadata(Group group, GroupPermissions permissions, IEnumerable<Profile> friends)
        {
            Group = new GroupModel(group);
            Permissions = permissions;
            return this;
        }
    }
}