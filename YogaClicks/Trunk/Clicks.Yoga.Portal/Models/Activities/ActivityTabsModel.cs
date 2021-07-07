using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityTabsModel
    {
        public ActivityTabsModel(ActivityPermissions permissions)
        {
            Permissions = permissions;
        }

        public ActivityPermissions Permissions { get; private set; }
    }
}