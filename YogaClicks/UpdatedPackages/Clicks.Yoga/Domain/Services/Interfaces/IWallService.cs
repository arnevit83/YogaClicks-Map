using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IWallService
    {
        WallPermissions GetPermissions(int wallId);
        Wall GetWall(int id);
        WallPost GetPost(int id);
        IList<WallPost> GetPosts(int wallId, DateTime time, int limit);
        WallPost CreatePost(int wallId, string content, IEnumerable<string> resourceUris);
        void SharePost(int postId);
        NewsStory ShareStory(int storyId);
        NewsStory ShareStory(NewsStory story);
        void DeletePost(int id);
        void DeleteResource(MediaResource resource);
    }
}