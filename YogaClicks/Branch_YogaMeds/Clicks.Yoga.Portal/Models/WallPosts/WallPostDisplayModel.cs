namespace Clicks.Yoga.Portal.Models.WallPosts
{
    public class WallPostDisplayModel
    {
        public WallPostDisplayModel(int postId)
        {
            PostId = postId;
        }

        public int PostId { get; private set; }
    }
}