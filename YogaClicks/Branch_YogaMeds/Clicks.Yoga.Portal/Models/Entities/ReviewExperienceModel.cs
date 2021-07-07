using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class ReviewExperienceModel : EntityModel<ReviewExperience>
    {
        public ReviewExperienceModel() {}

        public ReviewExperienceModel(ReviewExperience entity) : base(entity) {}

        public string Name { get; set; }

        public override void Populate(ReviewExperience entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}