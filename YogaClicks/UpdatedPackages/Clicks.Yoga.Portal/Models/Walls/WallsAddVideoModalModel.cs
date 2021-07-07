using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Walls
{
    public class WallsAddVideoModalModel
    {
        public WallsAddVideoModalModel(Video video)
        {
            Video = video;
        }

        public Video Video { get; set; }
    }
}