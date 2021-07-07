using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class StyleTraitGroup : Entity
    {
        public StyleTraitGroup()
        {
            Traits = new List<StyleTrait>();
        }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ICollection<StyleTrait> Traits { get; private set; }
    }
}