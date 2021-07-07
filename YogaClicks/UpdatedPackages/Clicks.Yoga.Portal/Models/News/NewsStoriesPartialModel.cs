using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.News
{
    public class NewsStoriesPartialModel
    {
        public NewsStoriesPartialModel(IEnumerable<NewsStory> stories)
        {
            Stories = new List<NewsStoryModel>();

            foreach (var story in stories)
            {
                Stories.Add(new NewsStoryModel(story));
            }
        }

        public IList<NewsStoryModel> Stories { get; private set; }
    }
}