using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Styles
{
    public class StyleIndexModel
    {
        public StyleIndexModel(IEnumerable<Style> styles)
        {
            Styles = new List<StyleModel>();

            foreach (var style in styles)
            {
                Styles.Add(new StyleModel(style));
            }
        }

        public IList<StyleModel> Styles { get; private set; }
    }
}