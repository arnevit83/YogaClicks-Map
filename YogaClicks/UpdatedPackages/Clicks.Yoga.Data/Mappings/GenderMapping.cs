using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GenderMapping : EntityMapping<Gender>
    {
        public GenderMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(7);
        }
    }
}