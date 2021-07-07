namespace Clicks.Yoga.Domain.Entities
{
    public class NewsPermissions
    {
        public bool Access { get; set; }

        public bool Post { get; set; }

        public bool Comment { get; set; }

        public bool Administrate { get; set; }
    }
}