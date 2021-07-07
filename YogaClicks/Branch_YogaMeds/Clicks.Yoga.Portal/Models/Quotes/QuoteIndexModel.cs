using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Quotes
{
    public class QuoteIndexModel
    {
        public QuoteIndexModel()
        {
            Quotes = new Dictionary<string, IList<QuoteModel>>();
            Authors = new List<string>();
        }

        public QuoteIndexModel(IEnumerable<Quote> quotes, string author)
            : this()
        {
            foreach (var quote in quotes)
            {
                var group = Quotes.ContainsKey(quote.Author) ? Quotes[quote.Author] : Quotes[quote.Author] = new List<QuoteModel>();
                group.Add(new QuoteModel(quote));
                if (!Authors.Contains(quote.Author)) Authors.Add(quote.Author);
                Author = author;
            }
        }

        public IList<string> Authors { get; private set; } 

        public IDictionary<string, IList<QuoteModel>> Quotes { get; private set; }

        public string Author { get; private set; }
    }
}