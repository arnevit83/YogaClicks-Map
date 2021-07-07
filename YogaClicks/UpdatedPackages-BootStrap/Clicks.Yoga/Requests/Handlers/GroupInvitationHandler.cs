using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class GroupInvitationHandler : IRequestCompletionHandler
    {
        public GroupInvitationHandler(IEntityContext entityContext, INewsService newsService, INotificationService notificationService)
        {
            EntityContext = entityContext;
            NewsService = newsService;
            NotificationService = notificationService;
        }

        private IEntityContext EntityContext { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        public void Accept(Request request)
        {
            var member = GetMember(request);

            if (member == null) return;

            member.Confirmed = true;

            NewsService.PostAction(NewsStoryType.FriendJoinedGroup, member.User.Profile, member.Group);

            NotificationService.Notify(member.Group.Owner, "GroupJoined", member.User.Profile, member.Group);
        }

        public void Reject(Request request)
        {
            var member = GetMember(request);

            if (member == null) return;

            EntityContext.GroupMembers.Remove(member);
        }

        private GroupMember GetMember(Request request)
        {
            return EntityContext.GroupMembers.Get(request.EntityId);
        }
    }
}