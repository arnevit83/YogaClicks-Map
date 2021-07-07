using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Context
{
    public class CurrentActorModel
    {
        public CurrentActorModel(EntityHandle handle)
        {
            Id = handle.EntityId;
            EntityType = new EntityTypeModel(handle.EntityType);
            Name = handle.Name;
            Image = new ImageModel(handle.Image);
        }

        public int Id { get; private set; }

        public EntityTypeModel EntityType { get; private set; }

        public string Name { get; private set; }

        public ImageModel Image { get; private set; }
    }
}