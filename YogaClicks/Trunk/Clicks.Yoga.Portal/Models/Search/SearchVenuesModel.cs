using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchVenuesModel : SearchResultsModel
    {
        public SearchVenuesModel(SearchCriteriaModel criteria) : base(criteria) {}

        public SearchVenuesModel(SearchCriteriaModel criteria, SearchResponse response) : base(criteria, response) {}
    }
}