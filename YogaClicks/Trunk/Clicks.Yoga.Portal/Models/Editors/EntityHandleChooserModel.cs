using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class EntityHandleChooserModel : MultipleEntitySelectorModel<EntityHandle, EntityHandleSummaryModel>
    {
        public EntityHandleChooserModel() {}

        public EntityHandleChooserModel(IEnumerable<EntityHandle> entities) : base(entities) {}
        protected override EntityHandleSummaryModel CreateModel(EntityHandle entity)
        {
            var model = base.CreateModel(entity);

            if (entity.EntityType.Name == "Teacher")
            {
                model.Name = model.Name + " (teacher)";
            }

            return model;
        }
    }
}