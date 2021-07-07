using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Data.Repositories
{
    public class StyleRepository : Repository<Style>, IStyleRepository
    {
        public StyleRepository(YogaDataContext context) : base(context) { }

        public override IQueryable<Style> WithRelated
        {
            get { return base.WithRelated.Include(e => e.Image); }
        }

        public IEnumerable<Style> GetByTraits(IEnumerable<int> traitIds)
        {
            return WithRelated
                .Include(e => e.Traits)
                .Where(style => !traitIds.Except(style.Traits.Select(trait => trait.Id)).Any())
                .OrderBy(e => e.Name);
        }
    }
}