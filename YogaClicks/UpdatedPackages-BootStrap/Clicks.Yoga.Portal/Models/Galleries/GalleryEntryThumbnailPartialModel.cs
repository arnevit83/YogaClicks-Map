using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryEntryThumbnailPartialModel
    {
        public GalleryEntryThumbnailPartialModel(GalleryEntry entry)
        {
            Entry = new GalleryEntryModel(entry);
        }

        public GalleryEntryThumbnailPartialModel(GalleryEntryModel entry)
        {
            Entry = entry;
        }

        public GalleryEntryModel Entry { get; private set; }
    }
}