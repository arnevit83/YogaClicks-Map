using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class QuoteMapping : EntityMapping<Quote>
    {
        public QuoteMapping()
        {
            Property(e => e.Author).IsRequired().HasMaxLength(100);
            Property(e => e.Quotation).IsRequired();
        }
    }
}