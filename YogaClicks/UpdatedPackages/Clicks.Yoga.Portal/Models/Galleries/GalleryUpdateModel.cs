using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryUpdateModel
    {
        public GalleryUpdateModel()
        {
            AssociateHandles = new EntityHandleChooserModel();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool Public { get; set; }

        public bool Open { get; set; }

        public EntityHandleChooserModel AssociateHandles { get; private set; }

        public void PopulateEntity(Gallery gallery, IGalleryService galleryService)
        {
            gallery.Name = Name != null ? Name.Substring(0, Math.Min(Name.Length, 100)) : "";
            gallery.Public = Public;
            gallery.Open = Open;

            foreach (var pair in AssociateHandles.Selection)
            {
                var handleId = Convert.ToInt32(pair.Key);

                if (pair.Value)
                {
                    galleryService.AssociateGallery(gallery, handleId);
                }
                else
                {
                    galleryService.DisassociateGallery(gallery, handleId);
                }
            }
        }
    }
}