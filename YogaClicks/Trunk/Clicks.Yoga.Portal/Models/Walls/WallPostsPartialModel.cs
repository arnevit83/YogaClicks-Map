using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Walls
{
    public class WallPostsPartialModel
    {
        public WallPostsPartialModel(IEnumerable<WallPost> posts, WallPermissions permissions)
        {
            Permissions = permissions;
            Posts = new List<WallPostModel>();

            foreach (var post in posts)
            {
                Posts.Add(new WallPostModel(post));
            }
        }

        public IList<WallPostModel> Posts { get; private set; }

        public WallPermissions Permissions { get; private set; }
    }
}