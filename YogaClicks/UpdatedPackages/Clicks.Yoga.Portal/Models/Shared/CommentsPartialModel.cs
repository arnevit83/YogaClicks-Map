using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Shared
{
    public class CommentsPartialModel
    {
        public CommentsPartialModel(EntityReference subject, IEnumerable<CommentModel> comments, CommentPermissions permissions)
        {
            Subject = subject;
            Permissions = permissions;
            Comments = new List<CommentModel>();

            foreach (var comment in comments)
            {
                Comments.Add(comment);
            }
        }

        public CommentsPartialModel(int subjectId, string subjectTypeName, IEnumerable<CommentModel> comments, CommentPermissions permissions)
            : this(new EntityReference(subjectId, subjectTypeName), comments, permissions)
        {}

        public EntityReference Subject { get; private set; }

        public IList<CommentModel> Comments { get; private set; }

        public CommentPermissions Permissions { get; private set; }
    }
}