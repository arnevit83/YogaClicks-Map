using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchTeachersModel : SearchResultsModel
    {
        public SearchTeachersModel(SearchCriteriaModel criteria) : base(criteria) {}

        public SearchTeachersModel(SearchCriteriaModel criteria, SearchResponse response) : base(criteria, response) {}
    }
}