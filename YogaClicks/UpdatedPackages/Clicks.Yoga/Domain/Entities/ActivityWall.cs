using System;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityWall : Wall
    {
        public virtual Activity Activity { get; set; }

        public override Type GetWallPermissionProviderType()
        {
            return typeof(ActivityWallPermissionProvider);
        }

        public override string GetWallPostNewsStoryTypeTag()
        {
            return NewsStoryType.ActivityPosted;
        }

        public override IEntityHandle GetWallContext()
        {
            return Activity;
        }
    }
}