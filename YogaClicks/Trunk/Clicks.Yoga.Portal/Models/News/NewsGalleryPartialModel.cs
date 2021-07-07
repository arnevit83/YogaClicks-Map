using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.News
{
    public class NewsGalleryPartialModel
    {
        public NewsGalleryPartialModel(NewsStory story, IEnumerable<GalleryEntry> entries)
        {
            Story = new NewsStoryModel(story);
            Entries = new List<GalleryEntryModel>();
            
            foreach (var entry in entries)
            {
                Entries.Add(new GalleryEntryModel(entry));
            }
        }

        public NewsStoryModel Story { get; private set; }

        public IList<GalleryEntryModel> Entries { get; private set; }
    }
}