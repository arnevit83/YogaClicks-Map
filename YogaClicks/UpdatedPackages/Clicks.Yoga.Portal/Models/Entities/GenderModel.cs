using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class GenderModel : EntityModel<Gender>
    {
        public GenderModel() {}

        public GenderModel(Gender gender) : base(gender) {}

        public string Name { get; set; }

        public override void Populate(Gender gender)
        {
            Id = gender.Id;
            Name = gender.Name;
        }
    }
}