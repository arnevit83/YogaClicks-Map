using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class EntityTypeModel : EntityModel<EntityType>
    {
        public EntityTypeModel() {}

        public EntityTypeModel(EntityType entity) : base(entity) {}

        public EntityTypeModel(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string DisplayPlural { get; set; }

        public string Controller { get; set; }

        public bool IsHuman { get; set; }

        public bool IsReviewable { get; set; }

        public bool CanReview { get; set; }

        public override void Populate(EntityType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            DisplayName = entity.DisplayName;
            DisplayPlural = entity.DisplayPlural;
            Controller = entity.Controller;
            IsHuman = entity.IsHuman;
            IsReviewable = entity.IsReviewable;
            CanReview = entity.CanReview;
        }
    }
}