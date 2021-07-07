using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryAssociatedPartialModel
    {
        public GalleryAssociatedPartialModel(int entityId, EntityType entityType, IEnumerable<Gallery> galleries, GalleryPermissions permissions)
        {
            EntityId = entityId;
            EntityType = new EntityTypeModel(entityType);
            Permissions = permissions;
            Galleries = new List<GalleryModel>();

            foreach (var gallery in galleries)
            {
                Galleries.Add(new GalleryModel(gallery));
            }
        }

        public int EntityId { get; private set; }

        public EntityTypeModel EntityType { get; private set; }

        public IList<GalleryModel> Galleries { get; private set; }

        public GalleryPermissions Permissions { get; private set; }
    }
}