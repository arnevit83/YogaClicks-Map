using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class Study : Entity
    {
        public string Title { get; set; }

        public string Abstract { get; set; }

        public string DateOfStudy { get; set; }

        public string Journal { get; set; }

        public string Volume { get; set; }

        public string Institution { get; set; }

        public string Source { get; set; }

        public int NumberOfCitations { get; set; }

        public virtual ICollection<TherapyType> TherapyTypes { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public bool Active { get; set; }
    }
}
