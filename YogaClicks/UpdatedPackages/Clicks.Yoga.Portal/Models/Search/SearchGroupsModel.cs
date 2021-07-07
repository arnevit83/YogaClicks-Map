using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchGroupsModel : SearchResultsModel
    {
        public SearchGroupsModel(SearchCriteriaModel criteria) : base(criteria) {}

        public SearchGroupsModel(SearchCriteriaModel criteria, SearchResponse response) : base(criteria, response) {}
    }
}