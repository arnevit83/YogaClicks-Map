using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class School : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
