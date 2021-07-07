using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class SearchResponse
    {
        public SearchResponse(IEnumerable<SearchResult> results, int totalCount, bool hasNextPage, bool hasPreviousPage)
        {
            Results = results;
            TotalCount = totalCount;
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;
        }

        public int TotalCount { get; private set; }

        public IEnumerable<SearchResult> Results { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }
    }
}