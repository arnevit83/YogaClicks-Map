using System;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class GroupWall : Wall
    {
        public virtual Group Group { get; set; }

        public override Type GetWallPermissionProviderType()
        {
            return typeof(GroupWallPermissionProvider);
        }

        public override string GetWallPostNewsStoryTypeTag()
        {
            return NewsStoryType.GroupPosted;
        }

        public override IEntityHandle GetWallContext()
        {
            return Group;
        }
    }
}