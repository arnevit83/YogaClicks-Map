using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GlossaryEntryMapping : EntityMapping<GlossaryEntry>
    {
        public GlossaryEntryMapping()
        {
            Property(e => e.Term).IsRequired().HasMaxLength(100);
            Property(e => e.Definition).IsRequired();
        }
    }
}