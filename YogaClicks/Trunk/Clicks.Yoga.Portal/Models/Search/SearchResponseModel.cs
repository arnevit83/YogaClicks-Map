using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchResponseModel
    {
        public SearchResponseModel()
        {
            Results = new List<SearchResultModel>();
        }

        public SearchResponseModel(SearchResponse response) : this()
        {
            TotalCount = response.TotalCount;
            HasNextPage = response.HasNextPage;
            HasPreviousPage = response.HasPreviousPage;

            foreach (var result in response.Results)
            {
                Results.Add(new SearchResultModel(result));
            }
        }

        public int TotalCount { get; private set; }

        public IList<SearchResultModel> Results { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }
    }
}