namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityTeacher : Entity
    {
        public virtual Activity Activity { get; set; }

        public virtual Teacher Teacher { get; set; }

        public bool Confirmed { get; set; }
    }
}