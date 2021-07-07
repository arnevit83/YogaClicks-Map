using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class StyleTrait : Entity
    {
        public string Name { get; set; }

        public virtual StyleTraitGroup Group { get; set; }

        public virtual ICollection<Style> Styles { get; set; }
    }
}