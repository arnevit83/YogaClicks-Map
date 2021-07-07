using System.Collections;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface ICommentService
    {
        CommentPermissions GetPermissions(EntityReference subject);
        Comment CreateComment(EntityReference subject, string content);
        void DeleteComment(EntityReference subject, int commentId);
        IEnumerable<Comment> GetComments(EntityReference subject);
        EntityReference GetActualSubject(EntityReference subject);
    }
}
