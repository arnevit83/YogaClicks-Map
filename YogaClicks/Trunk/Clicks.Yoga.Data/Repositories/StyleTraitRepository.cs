using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Data.Repositories
{
    public class StyleTraitRepository : Repository<StyleTrait>, IStyleTraitRepository
    {
        public StyleTraitRepository(YogaDataContext context) : base(context) { }

        public System.Collections.Generic.IEnumerable<StyleTrait> GetTraits()
        {
            Expression<Func<StyleTrait, object>> expression = e => e.Group;
            return WithRelated.Include(expression);

            return WithRelated.Include(e => e.Group).OrderBy(e => e.Name);
        }
    }
}