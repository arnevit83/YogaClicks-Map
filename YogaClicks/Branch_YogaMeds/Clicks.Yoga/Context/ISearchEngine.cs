using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Context
{
    public interface ISearchEngine
    {
        SearchResponse Search(SearchCriteria criteria);
        void Index(ISearchable subject);
        void Index(Review review);
        void Delete(ISearchable subject);
    }
}