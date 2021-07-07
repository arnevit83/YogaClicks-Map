using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class Author : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Study> Studies { get; set; }

        public bool Active { get; set; }
    }
}
