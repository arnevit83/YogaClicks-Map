using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IQuoteService
    {
        IList<Quote> GetQuotes();
        Quote GetQuoteForDisplay(int id);
    }
}