using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryCreateModel
    {
        public GalleryCreateModel()
        {
            Name = Gallery.DefaultName;
            AssociateHandles = new EntityHandleChooserModel();
        }

        public GalleryCreateModel(int entityId, string entityTypeName, IEnumerable<EntityHandle> associateOptions) : this()
        {
            EntityId = entityId;
            EntityTypeName = entityTypeName;

            AssociateHandles = new EntityHandleChooserModel();
            AssociateHandles.PopulateMetadata(associateOptions);

            var handle = associateOptions.FirstOrDefault(e => e.EntityId == entityId && e.EntityType.Name == entityTypeName);

            if (handle != null && AssociateHandles.Selection.ContainsKey(handle.Id)) AssociateHandles.Selection[handle.Id] = true;
        }

        public int EntityId { get; set; }

        public string EntityTypeName { get; set; }

        public string Name { get; set; }

        public IList<int> ImageIds { get; set; }

        public IList<string> ImageNames { get; set; }

        public bool Public { get; set; }

        public EntityHandleChooserModel AssociateHandles { get; set; }

        public void PopulateEntity(Gallery gallery, IGalleryService galleryService, IEntityService entityService)
        {
            gallery.Name = string.IsNullOrEmpty(Name) ? Gallery.DefaultName : Name.Substring(0, Math.Min(Name.Length, 100));
            gallery.Public = Public;

            if (ImageIds != null)
            {
                for (var i = 0; i < ImageIds.Count; i++)
                {
                    var id = ImageIds[i];
                    var name = ImageNames.Count > i ? ImageNames[i] ?? "" : "";

                    name = name.Substring(0, Math.Min(name.Length, 100));

                    galleryService.CreateEntry(gallery, id, name);
                }
            }

            var handle = entityService.EnsureEntityHandle(EntityId, EntityTypeName);

            galleryService.AssociateGallery(gallery, handle.Id);

            foreach (var id in AssociateHandles.Ids)
            {
                galleryService.AssociateGallery(gallery, id);
            }
        }
    }
}