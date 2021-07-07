using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Shared
{
    public class CommentPartialModel
    {
        public CommentPartialModel(EntityReference subject, Comment comment, CommentPermissions permissions)
        {
            Subject = subject;
            Comment = new CommentModel(comment);
            Permissions = permissions;
        }

        public CommentPartialModel(EntityReference subject, CommentModel comment, CommentPermissions permissions)
        {
            Subject = subject;
            Comment = comment;
            Permissions = permissions;
        }

        public EntityReference Subject { get; private set; }

        public CommentModel Comment { get; private set; }

        public CommentPermissions Permissions { get; private set; }
    }
}