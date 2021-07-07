using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class WhatTheScienceSays : Entity
    {
        public string Description { get; set; }

        public virtual ICollection<TherapyType> TherapyTypes { get; set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public bool Active { get; set; }
    }
}
