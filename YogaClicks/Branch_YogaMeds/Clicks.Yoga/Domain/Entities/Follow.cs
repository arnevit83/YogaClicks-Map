using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class Follow : Entity
    {
        public int FollowerId { get; set; }

        public EntityType FollowerType { get; set; }

        public virtual ICollection<Condition> Conditions { get; set; }

    }
}
