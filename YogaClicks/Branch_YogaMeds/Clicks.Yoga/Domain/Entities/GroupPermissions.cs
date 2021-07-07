namespace Clicks.Yoga.Domain.Entities
{
    public class GroupPermissions
    {
        public bool Access { get; set; }

        public bool Invite { get; set; }

        public bool Manage { get; set; }

        public bool ManageAdministrators { get; set; }
    }
}