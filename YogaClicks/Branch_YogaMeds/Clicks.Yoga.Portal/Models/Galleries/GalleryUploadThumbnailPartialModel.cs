using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryUploadThumbnailPartialModel
    {
        public GalleryUploadThumbnailPartialModel(Image image)
        {
            Image = new ImageModel(image);
        }

        public ImageModel Image { get; private set; }
    }
}