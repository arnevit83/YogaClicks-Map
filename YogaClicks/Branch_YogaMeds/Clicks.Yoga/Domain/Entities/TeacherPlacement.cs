using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherPlacement : Entity, IRelationship
    {
        public TeacherPlacement() {}

        public TeacherPlacement(Teacher teacher, Venue venue)
        {
            Teacher = teacher;
            Venue = venue;
        }

        public virtual Teacher Teacher { get; set; }

        public virtual Venue Venue { get; set; }

        public bool Confirmed { get; set; }

        IEnumerable<Entity> IRelationship.Targets
        {
            get
            {
                yield return Teacher;
                yield return Venue;
            }
        }
    }
}