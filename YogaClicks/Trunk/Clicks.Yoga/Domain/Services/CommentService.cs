using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Permissions;

namespace Clicks.Yoga.Domain.Services
{
    public class CommentService : ServiceBase, ICommentService
    {
        public CommentService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            INewsService newsService,
            INotificationService notificationService,
            ICommentPermissionProviderFactory permissionProviderFactory)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            NewsService = newsService;
            NotificationService = notificationService;
            PermissionProviderFactory = permissionProviderFactory;
        }

        private IEntityService EntityService { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        private ICommentPermissionProviderFactory PermissionProviderFactory { get; set; }

        public CommentPermissions GetPermissions(EntityReference subject)
        {
            var commentable = GetCommentable(subject);
            var permissions = GetPermissions(commentable);

            if (!SecurityContext.HasActor)
            {
                permissions.Create = false;
            }

            return permissions;
        }

        public Comment CreateComment(EntityReference subject, string content)
        {
            if (content == null)
                throw new ArgumentNullException("content");
            if (Regex.Split(content, @"\s+").Length > 300)
                throw new ArgumentException("Content must be 300 words or less.", "content");

            var subjectEntity = GetCommentable(subject);

            VerifyPermission(subjectEntity, p => p.Create);

            var comment = new Comment
            {
                Owner = SecurityContext.CurrentUser,
                ActorHandle = EntityService.EnsureEntityHandle(SecurityContext.CurrentActor),
                Content = content
            };

            subjectEntity.Comments.Add(comment);

            if (subjectEntity.Owner != null && subjectEntity.Owner.Id != SecurityContext.CurrentUser.Id)
            {
                NotificationService.Notify(subjectEntity.Owner, "CommentAdded", SecurityContext.CurrentActor, subjectEntity);
            }

            return comment;
        }

        public IEnumerable<Comment> GetComments(EntityReference subject)
        {
            var commentable = GetCommentable(subject);
            return commentable.Comments;
        }

        public EntityReference GetActualSubject(EntityReference subject)
        {
            return GetCommentable(subject).GetEntityReference();
        }

        public void DeleteComment(EntityReference subject, int commentId)
        {
            var subjectEntity = GetCommentable(subject);
            var comment = subjectEntity.Comments.FirstOrDefault(c => c.Id == commentId);

            if (!SecurityContext.IsOwner(comment))
            {
                VerifyPermission(subjectEntity, p => p.Delete);
            }

            subjectEntity.Comments.Remove(comment);
            EntityContext.Comments.Remove(comment);
        }

        private ICommentable GetCommentable(EntityReference reference)
        {
            var commentable = EntityService.GetEntity<ICommentable>(reference);

            if (commentable == null)
                throw new ArgumentException("Entity does not exist or does not implement ICommentable.");

            var actual = commentable.GetReferencedEntity();

            if (actual.HasValue)
            {
                commentable = EntityService.GetEntity<ICommentable>(actual.Value);

                if (commentable == null)
                    throw new ArgumentException("Entity does not exist or does not implement ICommentable.");
            }

            return commentable;
        }
       
        private CommentPermissions GetPermissions(ICommentable subject)
        {
            var provider = PermissionProviderFactory.GetProvider(subject);

            if (provider == null)
                throw new InvalidOperationException("Failed to obtain permission provider for subject.");

            return provider.GetPermissions(subject);
        }

        private void VerifyPermission(ICommentable subject, Func<CommentPermissions, bool> function)
        {
            var permissions = GetPermissions(subject);

            if (!function(permissions))
                throw new PermissionDeniedException();
        }

    }
}
