using System.Collections.Generic;
using System.Globalization;
using Clicks.Yoga.Domain.Entities;
using System.Linq;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchResultsModel
    {
        public SearchResultsModel(SearchCriteriaModel criteria)
        {
            Criteria = criteria;
            CategorisedResults = new Dictionary<string, List<SearchResultModel>>();
        }

        public SearchResultsModel(SearchCriteriaModel criteria, SearchResponse response)
            : this(criteria)
        {
            var stubbedTeachers = response.Results.Where(x => x.Stubbed && x.EntityTypeName == "Teacher");

            var filteredResponse = new SearchResponse(response.Results.Except(stubbedTeachers),
                response.TotalCount - stubbedTeachers.Count(),
                response.HasNextPage,
                response.HasPreviousPage);

            Response = new SearchResponseModel(filteredResponse);

            foreach (var result in filteredResponse.Results)
            {
                var model = new SearchResultModel(result);

                if (!CategorisedResults.ContainsKey(model.TypePlural))
                    CategorisedResults.Add(model.TypePlural, new List<SearchResultModel>());

                CategorisedResults[model.TypePlural].Add(model);
            }
        }

        public SearchCriteriaModel Criteria { get; private set; }

        public SearchResponseModel Response { get; private set; }

        public Dictionary<string, List<SearchResultModel>> CategorisedResults { get; private set; }

        public IList<SearchResultModel> Results
        {
            get { return Response.Results; }
        }

        public string PageDescription
        {
            get
            {
                if (Response.TotalCount == 1)
                    return "Result: 1";

                return string.Format("Results: {0}", Response.TotalCount);
            }
        }
    }
}