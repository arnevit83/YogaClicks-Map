using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class EntityHandleSelectorModel : SingleEntitySelectorModel<EntityHandle, EntityHandleSummaryModel>
    {
        public EntityHandleSelectorModel() { }

        public EntityHandleSelectorModel(EntityHandle entity) : base(entity) { }

        protected override EntityHandleSummaryModel CreateModel(EntityHandle entity)
        {
            var model = base.CreateModel(entity);

            if (entity.EntityType.Name == typeof(Teacher).Name)
            {
                model.Name = string.Format("{0} (teacher)", model.Name);
            }

            return model;
        }
    }
}