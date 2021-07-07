using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileUnclaimButtonModel
    {
        public ProfileUnclaimButtonModel(int entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = new EntityTypeModel(entityType);
        }

        public int EntityId { get; private set; }

        public EntityTypeModel EntityType { get; private set; }
    }
}