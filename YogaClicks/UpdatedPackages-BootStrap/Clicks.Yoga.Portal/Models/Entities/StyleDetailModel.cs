using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class StyleDetailModel : StyleModel
    {
        public StyleDetailModel(Style entity) : base(entity) {}

        public IList<StyleTraitModel> Traits { get; private set; }

        public override void Populate(Style entity)
        {
            Traits = new List<StyleTraitModel>();

            foreach (var trait in entity.Traits)
            {
                Traits.Add(new StyleTraitModel(trait, false));
            }

            base.Populate(entity);
        }
    }
}