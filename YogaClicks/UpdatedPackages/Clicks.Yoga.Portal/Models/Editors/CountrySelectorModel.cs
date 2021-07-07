using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class CountrySelectorModel : SingleEntitySelectorModel<Country, CountryModel>
    {
        public CountrySelectorModel() {}

        public CountrySelectorModel(Country entity) : base(entity) {}
    }
}