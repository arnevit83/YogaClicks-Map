using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupTabsModel
    {
        public GroupTabsModel(GroupPermissions permissions, GroupModel group)
        {
            Permissions = permissions;
            Group = group;
        }

        public GroupPermissions Permissions { get; private set; }
        public GroupModel Group { get; private set; }
    }
}