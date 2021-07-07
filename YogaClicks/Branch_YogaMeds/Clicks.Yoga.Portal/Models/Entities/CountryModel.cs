using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class CountryModel : EntityModel<Country>
    {
        public CountryModel() {}

        public CountryModel(Country entity) : base(entity) {}

        public string Code { get; set; }

        public string EnglishName { get; set; }

        public override void Populate(Country entity)
        {
            Id = entity.Id;
            Code = entity.Code;
            EnglishName = entity.EnglishName;
        }
    }
}