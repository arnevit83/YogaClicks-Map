namespace Clicks.Yoga.Domain.Entities
{
    public class WallPermissions
    {
        public bool Access { get; set; }

        public bool Post { get; set; }

        public bool Share
        {
            get { return Comment; }
        }

        public bool Comment { get; set; }

        public bool Administrate { get; set; }
    }
}