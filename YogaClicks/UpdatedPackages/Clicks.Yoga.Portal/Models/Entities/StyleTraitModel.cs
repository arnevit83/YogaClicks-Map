using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class StyleTraitModel : EntityModel<StyleTrait>
    {
        public StyleTraitModel(StyleTrait entity, bool selected)
            : base(entity)
        {
            Selected = selected;
        }

        public string Name { get; set; }

        public bool Selected { get; set; }

        public int Count { get; set; }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(Name); }
        }

        public override void Populate(StyleTrait entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}