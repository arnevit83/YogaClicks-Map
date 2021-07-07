using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.News
{
    public class NewsInteractionPartialModel
    {
        public NewsInteractionPartialModel(NewsStoryModel story, string message)
        {
            Story = story;
            Message = message;
        }

        public NewsStoryModel Story { get; private set; }

        public string Message { get; private set; }
    }
}