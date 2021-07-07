using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Styles
{
    public class StyleDisplayModel
    {
        public StyleDisplayModel(Style style, IEnumerable<Style> styles)
        {
            Style = new StyleDetailModel(style);
            Styles = new List<StyleModel>();

            foreach (var s in styles)
            {
                Styles.Add(new StyleModel(s));
            }
        }

        public StyleDetailModel Style { get; private set; }

        public IList<StyleModel> Styles { get; private set; }
    }
}