namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityVenue : Entity
    {
        public virtual Activity Activity { get; set; }

        public virtual Venue Venue { get; set; }

        public bool Confirmed { get; set; }
    }
}