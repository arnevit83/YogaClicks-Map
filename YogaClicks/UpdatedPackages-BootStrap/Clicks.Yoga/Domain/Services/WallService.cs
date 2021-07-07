using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Permissions;
using Common;

namespace Clicks.Yoga.Domain.Services
{
    public class WallService : ServiceBase, IWallService
    {
        public WallService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IFriendService friendService,
            IMediaService mediaService,
            INewsService newsService,
            INotificationService notificationService,
            IWallPermissionProviderFactory permissionProviderFactory)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            FriendService = friendService;
            MediaService = mediaService;
            NewsService = newsService;
            NotificationService = notificationService;
            PermissionProviderFactory = permissionProviderFactory;
        }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private IMediaService MediaService { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        private IWallPermissionProviderFactory PermissionProviderFactory { get; set; }

        public WallPermissions GetPermissions(int wallId)
        {
            var wall = GetWall(wallId);
            var permissions = GetPermissions(wall);

            if (!SecurityContext.HasActor)
            {
                permissions.Post = false;
                permissions.Comment = false;
            }

            return permissions;
        }

        public Wall GetWall(int id)
        {
            var wall = EntityContext.Walls.Get(id);
            if (wall == null) throw new UnknownEntityException();
            if (!wall.GetWallContext().Active) throw new UnknownEntityException();
            return wall;
        }

        public WallPost GetPost(int id)
        {
            var post = EntityContext.WallPosts.Get(id);
            if (post == null) throw new UnknownEntityException();
            if (!post.ActorHandle.Active) throw new UnknownEntityException();
            return post;
        }

        public IList<WallPost> GetPosts(int wallId, DateTime time, int limit)
        {
            return EntityContext.WallPosts
                .Include(e => e.ActorHandle)
                .Include(e => e.ActorHandle.Image)
                .Include(e => e.Resources)
                .Where(e => e.Wall.Id == wallId)
                .Where(e => e.ActorHandle.Active)
                .Where(e => e.CreationTime <= time)
                .OrderByDescending(e => e.CreationTime)
                .Take(limit)
                .ToList();
        }

        public WallPost CreatePost(int wallId, string content, IEnumerable<string> resourceUris)
        {
            if (content == null)
                throw new ArgumentNullException("content");
            if (Regex.Split(content, @"\s+").Length > 300)
                throw new ArgumentException("Content must be 300 words or less.", "content");
            if (resourceUris == null)
                throw new ArgumentNullException("resourceUris");

            var wall = GetWall(wallId);

            if (wall == null)
                throw new ArgumentOutOfRangeException("wallId");

            VerifyPermission(wall, p => p.Post);

            var actorHandle = EntityService.EnsureEntityHandle(SecurityContext.CurrentActor);

            var post = new WallPost
            {
                Owner = SecurityContext.CurrentUser,
                Wall = wall,
                ActorHandle = actorHandle,
                Content = content
            };

            wall.Posts.Add(post);

            foreach (var uri in resourceUris)
            {
                var resource = MediaService.CommitResource(uri);
                post.Resources.Add(resource);
            }

            CreatePostNewsStory(post, wall.GetWallPostNewsStoryTypeTag());

            return post;
        }

        public void SharePost(int postId)
        {
            var original = GetPost(postId);

            if (original == null)
                throw new UnknownEntityException();
            if (!SecurityContext.Authenticated)
                throw new PermissionDeniedException();
            if (!SharingPermitted(original))
                throw new PermissionDeniedException();

            var story = NewsService.GetStory(original.GetEntityReference());

            if (story == null)
            {
                story = CreatePostNewsStory(original, original.Wall.GetWallPostNewsStoryTypeTag());
            }

            ShareStory(story);
        }

        public NewsStory ShareStory(int storyId)
        {
            var story = EntityContext.NewsStories.Get(storyId);

            if (story == null)
                throw new ArgumentOutOfRangeException("storyId");

            return ShareStory(story);
        }

        public NewsStory ShareStory(NewsStory story)
        {
            if (story == null)
                throw new ArgumentNullException("story");
            if (!story.Type.Shareable)
                throw new PermissionDeniedException();
            if (!SharingPermitted(story))
                throw new PermissionDeniedException();

            var actorHandle = EntityService.EnsureEntityHandle(SecurityContext.CurrentActor);
            var wall = SecurityContext.CurrentProfile.Wall;

            var sharePost = new WallPost
            {
                Owner = SecurityContext.CurrentUser,
                Wall = wall,
                ActorHandle = actorHandle,
                Content = "",
                SharedStory = story.ShareOriginal ?? story
            };

            foreach (var resource in story.Resources)
            {
                sharePost.Resources.Add(resource);
            }

            wall.Posts.Add(sharePost);

            var shareStory = CreateShareNewsStory(sharePost);

            if (story.Actor.Owner != null)
            {
                NotificationService.Notify(story.Actor.Owner, "StoryShared", SecurityContext.CurrentActor, sharePost);
            }

            return shareStory;
        }

        public void DeletePost(int id)
        {
            var post = GetPost(id);

            if (!SecurityContext.IsOwner(post))
            {
                VerifyPermission(post.Wall, p => p.Comment);
            }

            foreach (var comment in post.Comments.ToList())
            {
                EntityContext.Comments.Remove(comment);
            }

            EntityContext.WallPosts.Remove(post);
        }

        public void DeleteResource(MediaResource resource)
        {
            var posts = EntityContext.WallPosts
                    .Where(p => p.Resources.Any(r => r.Id == resource.Id))
                    .ToList();

            foreach (var post in posts)
            {
                post.Resources.Remove(resource);

                if (post.Resources.Count == 0)
                {
                    EntityContext.WallPosts.Remove(post);
                }
            }
        }

        private WallPermissions GetPermissions(Wall wall)
        {
            var provider = PermissionProviderFactory.GetProvider(wall);

            if (provider == null)
                throw new InvalidOperationException("Failed to obtain permission provider for wall.");

            return provider.GetPermissions(wall);
        }

        private void VerifyPermission(Wall wall, Func<WallPermissions, bool> function)
        {
            var permissions = GetPermissions(wall);

            if (!function(permissions))
                throw new PermissionDeniedException();
        }

        private NewsStory CreatePostNewsStory(WallPost post, string storyTypeTag)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            var context = post.Wall.GetWallContext();
            return NewsService.PostAction(storyTypeTag, context, post.ActorHandle, context, post, post.Content, post.Resources);
        }

        private NewsStory CreateShareNewsStory(WallPost post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            var story = CreatePostNewsStory(post, NewsStoryType.FriendSharedStory);
            story.ShareOriginal = post.SharedStory;
            return story;
        }

        private bool SharingPermitted(WallPost post)
        {
            if (post == null)
                throw new ArgumentNullException("post");

            return
                SharingPermitted(post.Wall) &&
                SharingPermitted(post.Owner);
        }

        private bool SharingPermitted(Wall wall)
        {
            if (wall == null)
                throw new ArgumentNullException("wall");

            var permissions = GetPermissions(wall);
            return permissions.Share;
        }

        private bool SharingPermitted(NewsStory story)
        {
            if (story == null)
                throw new ArgumentNullException("story");

            WallPost post = null;

            if (story.EntityId.HasValue && story.EntityType != null && story.EntityType.Name == typeof(WallPost).Name)
            {
                post = EntityContext.WallPosts.Get(story.EntityId.Value);
            }

            return
                SharingPermitted(story.Actor.Owner) &&
                (post != null ? SharingPermitted(post) : SharingPermitted(story.Actor.Owner.Profile));
        }

        private bool SharingPermitted(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var preferences = user.PrivacyPreferences ?? PrivacyPreferences.Default;
            return preferences.SharingPermitted;
        }

        private bool SharingPermitted(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            return FriendService.GetFriendshipStatus(profile.Id, SecurityContext.CurrentProfile.Id) == FriendshipStatus.Friends;
        }
    }
}