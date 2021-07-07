using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class EntityHandleModel : EntityModel<IEntityHandle>, ISecurable
    {
        public EntityHandleModel() {}

        public EntityHandleModel(IEntityHandle entity) : base(entity) {}
        
        public EntityHandleModel(EntityHandle entity) : base(entity)
        {
            if (entity == null) return;
            EntityType = new EntityTypeModel(entity.EntityType);
        }

        public int EntityId { get; set; }

        public EntityTypeModel EntityType { get; set; }

        public string Name { get; set; }

        public ImageModel Image { get; set; }

        public string Location { get; set; }

        public int? OwnerId { get; set; }

        public bool Active { get; set; }

        public bool SharingPermitted { get; set; }

        public string Controller
        {
            get { return EntityType.Controller; }
        }

        public override void Populate(IEntityHandle entity)
        {
            EntityId = entity.Id;
            EntityType = new EntityTypeModel(entity.GetEntityTypeName());
            Name = entity.Name;
            if (entity.Image != null) Image = new ImageModel(entity.Image);
            Location = entity.Location;
            OwnerId = entity.Owner != null ? entity.Owner.Id : (int?)null;
            Active = entity.Active;
            SharingPermitted = ((entity.Owner != null ? entity.Owner.PrivacyPreferences : null) ?? PrivacyPreferences.Default).SharingPermitted;
        }
    }
}