using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupGalleriesModel
    {
        public GroupGalleriesModel(Group profile, GroupPermissions permissions)
        {
            Group = new GroupModel(profile);
            Permissions = permissions;
        }

        public GroupModel Group { get; private set; }

        public GroupPermissions Permissions { get; private set; }
    }
}