using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IRequestService
    {
        int GetPendingRequestCount(User user);
        int GetPendingFriendRequestCount(User user);
        IEnumerable<Request> GetPendingRequests(User user);
        IEnumerable<Request> GetPendingRequestsBySubject(int subjectId, string subjectTypeName, User user);
        IEnumerable<Request> GetPendingFriendRequests(User user);
        Request GetPendingRequest(int id);
        void Request(string typeTag, User user, IActor actor, IEntityHandle subject, IEntityHandle context, IEntity entity);
        void Accept(int requestId);
        void Accept(Request request);
        void Reject(int requestId);
        void Reject(Request request);
    }
}