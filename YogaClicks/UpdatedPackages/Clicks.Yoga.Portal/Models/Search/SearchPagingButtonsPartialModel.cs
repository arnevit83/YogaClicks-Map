using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchPagingButtonsPartialModel
    {
        public SearchPagingButtonsPartialModel(SearchCriteriaModel criteria, SearchResponseModel response)
        {
            Criteria = criteria;
            HasPages = response.HasNextPage || response.HasPreviousPage;
            HasNextPage = response.HasNextPage;
            HasPreviousPage = response.HasPreviousPage;

            if ((response.HasNextPage || response.HasPreviousPage) && Criteria.PageSize.HasValue)
            {
                TotalPages = (int)Math.Ceiling(response.TotalCount / (decimal)Criteria.PageSize.Value);
                CurrentPage = Criteria.PageIndex.HasValue ? Criteria.PageIndex.Value : 0;
            }
            else
            {
                TotalPages = 1;
                CurrentPage = 0;
            }

            var linkCount = Math.Min(9, TotalPages);
            var first = Math.Max(1, CurrentPage - 3);

            QuickLinks = new List<int>();

            for (var i = 0; i < linkCount; i++)
            {
                QuickLinks.Add(i + first);
            }
        }

        public SearchCriteriaModel Criteria { get; private set; }

        public bool HasPages { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }

        public int TotalPages { get; private set; }

        public int CurrentPage { get; private set; }

        public IList<int> QuickLinks { get; private set; }

        public int NextPageIndex
        {
            get { return Criteria.PageIndex.HasValue ? Criteria.PageIndex.Value + 1 : 1; }
        }

        public int PreviousPageIndex
        {
            get { return Criteria.PageIndex.Value - 1; }
        }

        public int PageSize
        {
            get { return Criteria.PageSize.HasValue ? Criteria.PageSize.Value : 0; }
        }
    }
}