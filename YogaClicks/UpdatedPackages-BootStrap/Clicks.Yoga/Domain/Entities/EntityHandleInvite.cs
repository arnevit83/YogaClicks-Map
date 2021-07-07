using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    public class EntityHandleInvite : Entity
    {
        public virtual EntityHandle EntityHandle { get; set; }

        public virtual User User { get; set; }

        public string EmailAddress { get; set; }
    }
}
