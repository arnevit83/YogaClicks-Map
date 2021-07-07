using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Walls
{
    public class WallPartialModel
    {
        public WallPartialModel(WallPermissions permissions)
        {
            Permissions = permissions;
        }

        public WallPermissions Permissions { get; private set; }
    }
}