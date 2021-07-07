using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Notifications
{
    public class FriendRequestsPopupPartialModel
    {
        public FriendRequestsPopupPartialModel(IEnumerable<Request> requests)
        {
            Requests = new List<RequestModel>();

            foreach (var request in requests)
            {
                Requests.Add(new RequestModel(request));
            }
        }

        public IList<RequestModel> Requests { get; private set; }
    }
}