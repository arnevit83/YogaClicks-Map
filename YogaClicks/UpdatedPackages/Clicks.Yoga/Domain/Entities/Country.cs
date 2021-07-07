namespace Clicks.Yoga.Domain.Entities
{
    public class Country : Entity
    {
        public string Code { get; set; }

        public string EnglishName { get; set; }

        public bool Visible { get; set; }
    }
}