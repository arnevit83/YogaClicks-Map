using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.Styles
{
    public class StyleTraitsNavigationModel
    {
        public StyleTraitsNavigationModel(IEnumerable<StyleTraitGroup> groups, IEnumerable<Style> styles, IList<int> selectedTraitIds)
        {
            Traits = new List<StyleTraitGroupModel>();
            SelectedTraits = new List<StyleTraitModel>();

            var lookup = new Dictionary<int, StyleTraitModel>();

            foreach (var group in groups)
            {
                var model = new StyleTraitGroupModel(group, selectedTraitIds);
                
                Traits.Add(model);

                foreach (var trait in model.Traits)
                    lookup.Add(trait.Id, trait);
            }

            foreach (var style in styles)
            {
                foreach (var trait in style.Traits)
                    lookup[trait.Id].Count += 1;
            }

            var traits = new List<StyleTraitModel>();

            foreach (var group in Traits)
                traits.AddRange(group.Traits.Where(t => t.Selected));

            SelectedTraits = traits;
        }

        public IList<StyleTraitGroupModel> Traits { get; private set; }

        public IList<StyleTraitModel> SelectedTraits { get; private set; }

        public string TraitIdString
        {
            get { return string.Join(WebUtility.UrlListSeparator, SelectedTraits.Select(e => e.Id.ToString())); }
        }

        public string TraitNameString
        {
            get { return string.Join(WebUtility.UrlListSeparator, SelectedTraits.Select(e => e.UrlSlug)); }
        }
    }
}