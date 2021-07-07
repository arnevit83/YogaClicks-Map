using System;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class ProfileWall : Wall
    {
        public virtual Profile Profile { get; set; }

        public override Type GetWallPermissionProviderType()
        {
            return typeof(ProfileWallPermissionProvider);
        }

        public override string GetWallPostNewsStoryTypeTag()
        {
            return NewsStoryType.FriendPosted;
        }

        public override IEntityHandle GetWallContext()
        {
            return Profile;
        }
    }
}