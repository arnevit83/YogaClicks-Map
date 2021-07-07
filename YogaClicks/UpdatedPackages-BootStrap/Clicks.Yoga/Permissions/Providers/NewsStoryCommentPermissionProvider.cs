using System;
using System.Security;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Permissions.Providers
{
    public class NewsStoryCommentPermissionProvider : ICommentPermissionProvider
    {
        public NewsStoryCommentPermissionProvider(ISecurityContext securityContext, INewsService newsService)
        {
            SecurityContext = securityContext;
            NewsService = newsService;
        }

        private ISecurityContext SecurityContext { get; set; }
        private INewsService NewsService { get; set; }

        public CommentPermissions GetPermissions(ICommentable target)
        {
            return new CommentPermissions
            {
                Create = SecurityContext.IsSuperUser() ||
                         SecurityContext.IsOwner(target.Owner) ||
                         NewsService.IsSubscribed(target.GetReferencedEntity() ?? target.GetEntityReference()),
                Delete = false
            };
        }
    }
}