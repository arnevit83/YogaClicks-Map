using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class QuoteService : ServiceBase, IQuoteService
    {
        public QuoteService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public IList<Quote> GetQuotes()
        {
            return EntityContext.Quotes.OrderBy(e => e.Author).ToList();
        }

        public Quote GetQuoteForDisplay(int id)
        {
            var quote = EntityContext.Quotes.Get(id);
            if (quote == null) throw new UnknownEntityException();
            return quote;
        }
    }
}