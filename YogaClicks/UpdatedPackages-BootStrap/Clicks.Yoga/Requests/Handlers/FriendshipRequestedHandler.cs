using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Requests.Handlers
{
    public class FriendshipRequestedHandler : IRequestCompletionHandler
    {
        public FriendshipRequestedHandler(IFriendService friendService)
        {
            FriendService = friendService;
        }

        private IFriendService FriendService { get; set; }
        
        public void Accept(Request request)
        {
            try
            {
                FriendService.Accept(request.User.Profile.Id, request.EntityId);
            }
            catch (FriendshipStatusException) {}
        }

        public void Reject(Request request)
        {
            FriendService.Reject(request.User.Profile.Id, request.EntityId);
        }
    }
}