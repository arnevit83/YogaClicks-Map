using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryDisplayPartialModel
    {
        public GalleryDisplayPartialModel(Gallery gallery, IEnumerable<GalleryEntry> entries, IEnumerable<EntityHandle> associateOptions, int entityId, EntityType entityType)
        {
            EntityId = entityId;
            EntityType = new EntityTypeModel(entityType);
            Gallery = new GalleryModel(gallery);
            Entries = new List<GalleryEntryModel>();
            AssociateHandles = new EntityHandleChooserModel(gallery.Associations.Select(a => a.Handle));

            foreach (var entry in entries)
            {
                Entries.Add(new GalleryEntryModel(entry));
            }

            if (associateOptions.Any())
            {
                AssociateHandles.PopulateMetadata(associateOptions);
            }
            
        }

        public int EntityId { get; private set; }

        public EntityTypeModel EntityType { get; private set; }

        public GalleryModel Gallery { get; private set; }

        public IList<GalleryEntryModel> Entries { get; private set; }

        public EntityHandleChooserModel AssociateHandles { get; private set; }
    }
}