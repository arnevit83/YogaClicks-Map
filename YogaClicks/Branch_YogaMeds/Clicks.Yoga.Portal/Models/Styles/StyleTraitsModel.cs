using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Styles
{
    public class StyleTraitsModel
    {
        public StyleTraitsModel(IEnumerable<Style> styles, string traitIdString, string traitNameString)
        {
            Styles = new List<StyleModel>();
            TraitIdString = traitIdString;
            TraitNameString = traitNameString;

            foreach (var style in styles)
            {
                Styles.Add(new StyleModel(style));
            }
        }

        public IList<StyleModel> Styles { get; private set; }

        public string TraitIdString { get; set; }
        public string TraitNameString { get; set; }
    }
}