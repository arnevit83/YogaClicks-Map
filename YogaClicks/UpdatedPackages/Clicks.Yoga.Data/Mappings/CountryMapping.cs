using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class CountryMapping : EntityMapping<Country>
    {
        public CountryMapping()
        {
            Property(e => e.Code).IsRequired().IsFixedLength().HasMaxLength(2);
            Property(e => e.EnglishName).IsRequired().HasMaxLength(50);
        }
    }
}