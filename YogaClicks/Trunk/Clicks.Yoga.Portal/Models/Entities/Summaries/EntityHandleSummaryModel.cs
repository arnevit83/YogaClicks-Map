using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities.Summaries
{
    public class EntityHandleSummaryModel : EntityModel<EntityHandle>
    {
        public string Name { get; set; }

        public override void Populate(EntityHandle entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}