namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryUpdateEntryModel
    {
        public int EntryId { get; set; }

        public string Name { get; set; }

        public bool? IsCover { get; set; }

        public bool? IsPromoted { get; set; }
    }
}