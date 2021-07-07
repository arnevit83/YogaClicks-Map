using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchIndexModel
    {
        public SearchIndexModel(SearchCriteriaModel criteria)
        {
            Criteria = criteria == null ? new SearchCriteriaModel() : criteria;
            Styles = new List<StyleModel>();
        }

        public SearchCriteriaModel Criteria { get; set; }

        public IList<StyleModel> Styles { get; private set; }

        public void PopulateMetadata(IEnumerable<Style> styles)
        {
            foreach (var style in styles)
            {
                Styles.Add(new StyleModel(style));
            }
        }
    }
}