using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupMembersModel
    {
        public GroupMembersModel(Group group, GroupPermissions permissions)
        {
            Group = new GroupModel(group);
            Permissions = permissions;
        }

        public GroupModel Group { get; private set; }

        public GroupPermissions Permissions { get; private set; }
    }
}