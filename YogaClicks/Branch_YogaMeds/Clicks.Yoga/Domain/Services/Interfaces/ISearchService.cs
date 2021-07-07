using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface ISearchService
    {
        void PrepareCriteria(SearchCriteria criteria);
        SearchResponse Search(SearchCriteria criteria);
        void Index(ISearchable subject);
        void Index(Review review);
        void Remove(ISearchable subject);
        void IndexElastic(ISearchable subject);
    }
}