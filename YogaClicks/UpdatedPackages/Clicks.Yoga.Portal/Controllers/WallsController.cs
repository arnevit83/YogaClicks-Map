using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Walls;

namespace Clicks.Yoga.Portal.Controllers
{
    public class WallsController : YogaController
    {
        public WallsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IWallService wallService)
            : base(workUnit, securityContext)
        {
            WallService = wallService;
        }

        private IWallService WallService { get; set; }

        public ActionResult WallPartial(int id)
        {
            var permissions = WallService.GetPermissions(id);
            return PartialView(new WallPartialModel(permissions));
        }

        public ActionResult PostsPartial(int id, DateTime? time = null, int count = 20)
        {
            if (time == null) time = DateTime.Now;
            var posts = WallService.GetPosts(id, time.Value, count);
            var permissions = WallService.GetPermissions(id);
            return PartialView(new WallPostsPartialModel(posts, permissions));
        }

        public ActionResult PostPartial(int postId)
        {
            var post = WallService.GetPost(postId);
            var permissions = WallService.GetPermissions(post.Wall.Id);
            
            if (!permissions.Access)
                throw new PermissionDeniedException();

            return PartialView(new WallPostPartialModel(post, permissions));
        }

        public ActionResult Post(int id, string content, IEnumerable<string> resourceUris)
        {
            if (!SecurityContext.HasActor) return new EmptyResult();
            
            var post = WallService.CreatePost(id, content, resourceUris ?? new List<string>());
            var permissions = WallService.GetPermissions(id);

            WorkUnit.Commit();

            return View("PostPartial", new WallPostPartialModel(post, permissions));
        }

        public ActionResult Share(int id, int postId)
        {
            if (!SecurityContext.HasActor) return new EmptyResult();

            WallService.SharePost(postId);
            WorkUnit.Commit();

            return new EmptyResult();
        }

        public ActionResult DeletePost(int id, int postId)
        {
            WallService.DeletePost(postId);
            WorkUnit.Commit();
            
            return new EmptyResult();
        }

        public ActionResult AddVideoModal()
        {
            return View("AddVideoModal", new WallsAddVideoModalModel(null));
        }
    }
}
