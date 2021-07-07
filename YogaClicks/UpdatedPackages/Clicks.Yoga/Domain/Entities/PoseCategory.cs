using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class PoseCategory : Entity
    {
        public string Name { get; set; }

        public string Caption { get; set; }

        public int SortKey { get; set; }

        public virtual ICollection<Pose> Poses { get; set; }
    }
}
