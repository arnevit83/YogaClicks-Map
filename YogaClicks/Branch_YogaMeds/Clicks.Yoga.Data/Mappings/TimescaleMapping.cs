using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class TimescaleMapping : EntityMapping<Timescale>
    {
        public TimescaleMapping()
        {
            Property(e => e.Name).IsRequired();
            Property(e => e.IsDay).IsRequired();
            Property(e => e.IsWeek).IsRequired();
            Property(e => e.IsMonth).IsRequired();
        }
    }
}