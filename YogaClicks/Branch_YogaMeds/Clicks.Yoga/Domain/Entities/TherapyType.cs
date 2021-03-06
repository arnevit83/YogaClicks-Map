using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class TherapyType : Entity
    {
        public string Name { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<Study> Studies { get; set; }

        public virtual ICollection<WhatTheScienceSays> WhatTheScienceSayses { get; set; }
    }
}
