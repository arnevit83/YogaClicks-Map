using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchProfilesModel
    {
        public SearchProfilesModel()
        {
            Criteria = new SearchCriteriaModel();
            Response = new SearchResponseModel();
        }

        public SearchProfilesModel(SearchCriteriaModel model, SearchResponse response)
        {
            Criteria = model;
            Response = new SearchResponseModel(response);
        }

        public SearchCriteriaModel Criteria { get; set; }

        public SearchResponseModel Response { get; private set; }
    }
}