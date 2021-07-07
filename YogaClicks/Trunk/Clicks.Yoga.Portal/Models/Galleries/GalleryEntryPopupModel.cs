using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryEntryPopupModel
    {
        public GalleryEntryPopupModel(GalleryEntry entry)
        {
            Gallery = new GalleryModel(entry.Gallery);
            Entry = new GalleryEntryModel(entry);
            IsCover = entry.Gallery.Cover != null && entry.Gallery.Cover.Id == entry.Image.Id;

            var next = entry.Next();
            var prev = entry.Previous();
            var first = entry.First();
            var last = entry.Last();

            if (next != null)
                NextEntryId = next.Id;
            else if (first != null)
                NextEntryId = first.Id;

            if (prev != null)
                PreviousEntryId = prev.Id;
            else if (last != null)
                PreviousEntryId = last.Id;
        }

        public GalleryModel Gallery { get; private set; }

        public GalleryEntryModel Entry { get; private set; }

        public int? PreviousEntryId { get; private set; }

        public int? NextEntryId { get; private set; }

        public bool IsCover { get; private set; }
    }
}