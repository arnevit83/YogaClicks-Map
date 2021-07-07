using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Shared;

namespace Clicks.Yoga.Portal.Controllers
{
    public class CommentsController : YogaController
    {
        public CommentsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            ICommentService commentService)
            : base(workUnit, securityContext)
        {
            CommentService = commentService;
        }

        private ICommentService CommentService { get; set; }

        public ActionResult Create(EntityReference subject, string content)
        {
            if (!SecurityContext.HasActor) return new EmptyResult();

            var comment = CommentService.CreateComment(subject, content);
            var permissions = CommentService.GetPermissions(subject);

            WorkUnit.Commit();

            return View("CommentPartial", new CommentPartialModel(subject, comment, permissions));
        }

        public ActionResult Delete(EntityReference subject, int commentId)
        {
            CommentService.DeleteComment(subject, commentId);

            WorkUnit.Commit();

            return new EmptyResult();
        }

        public ActionResult CommentsPartial(EntityReference subject)
        {
            if (!SecurityContext.HasActor) return new EmptyResult();

            var permissions = CommentService.GetPermissions(subject);
            var comments = CommentService.GetComments(subject);

            var models = comments.Select(c => new CommentModel(c));

            return PartialView("CommentsPartial", new CommentsPartialModel(subject.EntityId, "NewsStory", models, permissions));
        }
    }
}