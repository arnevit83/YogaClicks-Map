using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Styles
{
    public class StyleDirectoryNavigationModel
    {
        public StyleDirectoryNavigationModel(IEnumerable<Style> styles)
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