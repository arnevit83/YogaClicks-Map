namespace Clicks.Yoga.Domain.Entities
{
    public class Timescale : Entity
    {
        public string Tag { get; set; }

        public string Name { get; set; }

        public bool IsDay { get; set; }

        public bool IsWeek { get; set; }

        public bool IsMonth { get; set; }
    }
}