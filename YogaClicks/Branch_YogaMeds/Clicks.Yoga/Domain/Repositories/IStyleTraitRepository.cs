using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Repositories
{
    public interface IStyleTraitRepository : IRepository<StyleTrait>
    {
        IEnumerable<StyleTrait> GetTraits();
    }
}