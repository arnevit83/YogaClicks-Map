using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Glossary
{
    public class GlossaryIndexModel
    {
        public GlossaryIndexModel(IEnumerable<GlossaryEntry> entries, string term)
        {
            Entries = new List<GlossaryEntryModel>(entries.Select(e => new GlossaryEntryModel(e)));
            Term = term;
        }

        public IList<GlossaryEntryModel> Entries { get; private set; }

        public string Term { get; private set; }
    }
}