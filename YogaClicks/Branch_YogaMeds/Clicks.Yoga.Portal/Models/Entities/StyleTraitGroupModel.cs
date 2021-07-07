using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class StyleTraitGroupModel : EntityModel<StyleTraitGroup>
    {
        public StyleTraitGroupModel(StyleTraitGroup entity, IList<int> selectedTraitIds)
            : base(entity)
        {
            Traits = new List<StyleTraitModel>();

            foreach (var trait in entity.Traits.OrderBy(t => t.Name))
            {
                Traits.Add(new StyleTraitModel(trait, selectedTraitIds.Contains(trait.Id)));
            }
        }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public IList<StyleTraitModel> Traits { get; private set; }

        public override void Populate(StyleTraitGroup entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            DisplayOrder = entity.DisplayOrder;
        }
    }
}