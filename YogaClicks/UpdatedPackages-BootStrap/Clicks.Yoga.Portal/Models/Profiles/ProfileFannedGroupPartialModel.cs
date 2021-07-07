using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileFannedGroupPartialModel
    {
        public ProfileFannedGroupPartialModel(EntityType entityType, IEnumerable<EntityHandle> entityHandles, bool editable)
        {
            EntityType = new EntityTypeModel(entityType);
            Editable = editable;
            EntityHandles = new List<EntityHandleModel>();

            foreach (var handle in entityHandles)
            {
                EntityHandles.Add(new EntityHandleModel(handle));
            }
        }

        public ProfileFannedGroupPartialModel(EntityTypeModel entityType, IEnumerable<EntityHandleModel> entityHandles, bool editable)
        {
            EntityType = entityType;
            Editable = editable;
            EntityHandles = new List<EntityHandleModel>();
            
            foreach (var handle in entityHandles)
            {
                EntityHandles.Add(handle);
            }
        }

        public bool Editable { get; private set; }

        public EntityTypeModel EntityType { get; private set; }

        public IList<EntityHandleModel> EntityHandles { get; private set; }
    }
}