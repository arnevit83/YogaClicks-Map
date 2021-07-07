using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class GlossaryService : ServiceBase, IGlossaryService
    {
        public GlossaryService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext)
        {
        }

        public IList<GlossaryEntry> GetGlossaryEntries()
        {
            return EntityContext.GlossaryEntries.OrderBy(e => e.Term).ToList();
        }

        public GlossaryEntry GetGlossaryEntryForDisplay(string term)
        {
            var entry = EntityContext.GlossaryEntries.FirstOrDefault(e => e.Term == term);
            if (entry == null) throw new UnknownEntityException();
            return entry;
        }
    }
}