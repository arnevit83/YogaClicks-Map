using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Requests;

namespace Clicks.Yoga.Domain.Services
{
    public class RequestService : ServiceBase, IRequestService
    {
        public RequestService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IRequestCompletionHandlerFactory handlerFactory)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            HandlerFactory = handlerFactory;
        }

        private IEntityService EntityService { get; set; }

        private IRequestCompletionHandlerFactory HandlerFactory { get; set; }

        public int GetPendingRequestCount(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.Requests
                .Where(e => e.User.Id == user.Id)
                .Where(e => !e.Completed)
                .Where(e => !e.Type.IsFriend)
                .Where(e => e.ActorHandle == null || e.ActorHandle.Active)
                .Count(e => e.SubjectHandle == null || e.SubjectHandle.Active);
        }

        public int GetPendingFriendRequestCount(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.Requests
                .Where(e => e.User.Id == user.Id)
                .Where(e => !e.Completed)
                .Where(e => e.Type.IsFriend)
                .Where(e => e.ActorHandle.Active)
                .Count(e => e.SubjectHandle.Active);
        }

        public IEnumerable<Request> GetPendingRequests(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.Requests
                .Where(e => e.User.Id == user.Id)
                .Where(e => !e.Completed)
                .Where(e => !e.Type.IsFriend)
                .Where(e => e.ActorHandle.Active)
                .Where(e => e.SubjectHandle.Active)
                .OrderByDescending(e => e.CreationTime)
                .ToList();
        }

        public IEnumerable<Request> GetPendingRequestsBySubject(int subjectId, string subjectTypeName, User user)
        {
            if (subjectTypeName == null)
                throw new ArgumentNullException("subjectTypeName");
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.Requests
                .Where(e => e.SubjectHandle.EntityId == subjectId)
                .Where(e => e.SubjectHandle.EntityType.Name == subjectTypeName)
                .Where(e => e.SubjectHandle.Active)
                .Where(e => e.User.Id == user.Id)
                .Where(e => !e.Completed)
                .Where(e => !e.Type.IsFriend)
                .OrderByDescending(e => e.CreationTime)
                .ToList();
        }

        public IEnumerable<Request> GetPendingFriendRequests(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return EntityContext.Requests
                .Where(e => e.User.Id == user.Id)
                .Where(e => !e.Completed)
                .Where(e => e.Type.IsFriend)
                .Where(e => e.ActorHandle.Active)
                .Where(e => e.SubjectHandle.Active)
                .OrderByDescending(e => e.CreationTime)
                .ToList();
        }

        public Request GetPendingRequest(int id)
        {
            var request = EntityContext.Requests.FirstOrDefault(e => e.Id == id && !e.Completed);
            if (request == null) throw new UnknownEntityException();
            return request;
        }

        public void Request(string typeTag, User user, IActor actor, IEntityHandle subject, IEntityHandle context, IEntity entity)
        {
            if (typeTag == null)
                throw new ArgumentNullException("typeTag");
            if (user == null)
                throw new ArgumentNullException("user");
            if (actor == null)
                throw new ArgumentNullException("actor");

            var type = EntityContext.RequestTypes.Get(e => e.Tag == typeTag);

            if (type == null)
                throw new ArgumentOutOfRangeException("typeTag");

            EntityHandle actorHandle = null;
            EntityHandle subjectHandle = null;
            EntityHandle contextHandle = null;

            actorHandle = EntityService.EnsureEntityHandle(actor);

            if (subject != null) subjectHandle = EntityService.EnsureEntityHandle(subject);
            if (context != null) contextHandle = EntityService.EnsureEntityHandle(context);

            var request = new Request
            {
                Type = type,
                User = user,
                ActorHandle = actorHandle,
                SubjectHandle = subjectHandle,
                ContextHandle = contextHandle
            };

            if (entity != null)
            {
                if (entity.IsTransient)
                {
                    EntityContext.RegisterTransientEntityDependency(request, e => e.EntityId, entity);
                }
                else
                {
                    request.EntityId = entity.Id;
                }
            }

            EntityContext.Requests.Add(request);
        }

        public void Accept(int requestId)
        {
            Complete(requestId, true);
        }

        public void Accept(Request request)
        {
            Complete(request, true);
        }

        public void Reject(int requestId)
        {
            Complete(requestId, false);
        }

        public void Reject(Request request)
        {
            Complete(request, false);
        }

        private void Complete(int requestId, bool accepted)
        {
            var request = EntityContext.Requests.Get(requestId);

            if (request == null)
                throw new UnknownEntityException();

            Complete(request, accepted);
        }

        private void Complete(Request request, bool accepted)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (request.Completed)
                throw new InvalidOperationException("Request has already been completed.");
            if (!SecurityContext.CanUpdate(request.User))
                throw new PermissionDeniedException();

            if (request.Completed) return;

            var handler = HandlerFactory.GetHandler(request.Type);

            if (handler == null) return;

            if (accepted)
            {
                handler.Accept(request);
            }
            else
            {
                handler.Reject(request);
            }

            request.Completed = true;
            request.CompletionTime = DateTime.Now;
        }
    }
}