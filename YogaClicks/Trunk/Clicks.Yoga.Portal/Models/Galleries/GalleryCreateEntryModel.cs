using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryCreateEntryModel
    {
        public GalleryCreateEntryModel()
        {
            Image = new ImageUploadModel();
        }

        public string Name { get; set; }

        public ImageUploadModel Image { get; private set; }

        public void PopulateEntity(GalleryEntry entry, IImageService imageService)
        {
            entry.Name = Name;
            entry.Image = Image.PopulateEntity(() => entry.Image, imageService);
        }
    }
}