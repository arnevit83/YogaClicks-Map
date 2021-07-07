using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupWallModel
    {
        public GroupWallModel(Group group, GroupPermissions permissions)
        {
            Group = new GroupModel(group);
            Permissions = permissions;
        }

        public GroupModel Group { get; private set; }

        public GroupPermissions Permissions { get; private set; }
    }
}