using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class EntityTypeChooserModel : MultipleEntitySelectorModel<EntityType, EntityTypeModel>
    {
        public EntityTypeChooserModel() {}

        public EntityTypeChooserModel(IEnumerable<EntityType> entities) : base(entities) {}

        public EntityTypeChooserModel(IEnumerable<EntityType> selections, IEnumerable<EntityType> options) : base(selections, options) {}

        public void PopulateCollection(ICollection<EntityType> collection, IEntityService entityService)
        {
            PopulateCollection(collection, entityService.GetEntityType);
        }
    }
}