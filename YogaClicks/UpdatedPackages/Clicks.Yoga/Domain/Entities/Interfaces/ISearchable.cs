using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public interface ISearchable : IEntity
    {
        bool IsSearchable { get; }
        void PopulateSearchRecord(SearchRecord record);
        IEnumerable<ISearchable> GetChildSearchables();
    }
}