using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class UserStory : PrincipalEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

        public bool OwnerHidden { get; set; }

        public bool Active { get; set; }
    }
}
