using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IStyleService
    {
        Style GetStyle(int id);
        IList<Style> GetStyles();
        IList<Style> GetStylesForInfiniteScroll(int skip, int take);
        IList<Style> GetStylesByTraits(IEnumerable<int> traitIds);
        IList<Style> GetStylesByTraitsForInfiniteScroll(IEnumerable<int> traitIds, int skip, int take);
        StyleTrait GetTrait(int id);
        IList<StyleTrait> GetTraits();
    }
}