using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Repositories
{
    public interface IStyleRepository : IRepository<Style>
    {
        IEnumerable<Style> GetByTraits(IEnumerable<int> traitIds);
    }
}