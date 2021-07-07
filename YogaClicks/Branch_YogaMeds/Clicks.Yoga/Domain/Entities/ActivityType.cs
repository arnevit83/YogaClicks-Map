namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityType : Entity
    {
        public string Name { get; set; }

        public bool IsClass { get; set; }

        public bool IsEvent { get; set; }
    }
}