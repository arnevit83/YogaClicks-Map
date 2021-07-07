using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IGlossaryService
    {
        IList<GlossaryEntry> GetGlossaryEntries();
        GlossaryEntry GetGlossaryEntryForDisplay(string term);
    }
}