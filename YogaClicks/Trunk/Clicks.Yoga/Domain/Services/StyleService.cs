using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class StyleService : ServiceBase, IStyleService
    {
        public StyleService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public Style GetStyle(int id)
        {
            return EntityContext.Styles.Get(e => e.Id == id);
        }

        public IList<Style> GetStyles()
        {
            return EntityContext.Styles.OrderBy(e => e.Name).ToList();
        }

        public IList<Style> GetStylesForInfiniteScroll(int skip, int take)
        {
            return EntityContext.Styles
                .OrderBy(e => e.Name)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public IList<Style> GetStylesByTraits(IEnumerable<int> traitIds)
        {
            return EntityContext.Styles.GetByTraits(traitIds).ToList();
        }

        public IList<Style> GetStylesByTraitsForInfiniteScroll(IEnumerable<int> traitIds, int skip, int take)
        {
            return EntityContext.Styles
                .GetByTraits(traitIds)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public StyleTrait GetTrait(int id)
        {
            return EntityContext.StyleTraits.Get(e => e.Id == id);
        }

        public IList<StyleTrait> GetTraits()
        {
            return EntityContext.StyleTraits.GetTraits().ToList();
        }
    }
}