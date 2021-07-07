using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Antlr.Runtime.Tree;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.News;
using System.Configuration;

namespace Clicks.Yoga.Portal.Controllers
{
    public class NewsController : YogaController
    {
        public NewsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IGalleryService galleryService,
            INewsService newsService,
            IReviewService reviewService,
            IWallService wallService,
            IEntityService entityService,
            IVideoService videoService,
            IFanService fanService)
            : base(workUnit, securityContext)
        {
            GalleryService = galleryService;
            NewsService = newsService;
            ReviewService = reviewService;
            WallService = wallService;
            EntityService = entityService;
            VideoService = videoService;
            FanService = fanService;
        }

        private IWallService WallService { get; set; }

        private IGalleryService GalleryService { get; set; }

        private INewsService NewsService { get; set; }

        private IReviewService ReviewService { get; set; }

        private IEntityService EntityService { get; set; }

        private IVideoService VideoService { get; set; }

        private IFanService FanService { get; set; }

        [YogaAuthorize]
        public ActionResult StoriesPartial(int? earliest, int? latest, int skip = 0, int take = -1)
        {
            if (take == -1) take = int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.InfiniteScroll.DefaultNumberOfNewsStories"]);
            var stories = NewsService.GetStories(SecurityContext.CurrentProfile.Id, earliest, latest, take, skip);

            return View(new NewsStoriesPartialModel(stories));
        }

        public ActionResult GalleryPartial(int storyId)
        {
            var story = NewsService.GetStory(storyId);

            if (story == null) return new EmptyResult();

            var gallery = GalleryService.GetGallery(story.Target.EntityId);
            var visible = GalleryService.GalleryVisible(gallery);

            if (visible)
            {
                var startTime = story.CreationTime.AddSeconds(-1);
                var endTime = story.ModificationTime.AddSeconds(1);

                var entries = gallery.Entries.Where(e => e.CreationTime >= startTime && e.CreationTime < endTime);

                return View(new NewsGalleryPartialModel(story, entries));
            }
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult ReviewPartial(int storyId)
        {
            var story = NewsService.GetStory(storyId);

            if (story == null || !story.EntityId.HasValue) return new EmptyResult();

            var review = ReviewService.GetReview(story.EntityId.Value);

            if (review == null) return new EmptyResult();

            return View(new NewsReviewPartialModel(review));
        }

        public ActionResult Share(int storyId)
        {
            if (!SecurityContext.HasActor) return new EmptyResult();

            var story = WallService.ShareStory(storyId);
            WorkUnit.Commit();

            return View("StoryPartial", new NewsStoryModel(story));
        }

        public ActionResult DeletePost(int postId)
        {
            WallService.DeletePost(postId);
            WorkUnit.Commit();

            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateVideo(string url)
        {
            Video video = null;

            try
            {
                video = VideoService.CreateVideo(url);
            }
            catch (ArgumentException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            return Json(new {video.EmbedHtml, url});
        }

    }
}