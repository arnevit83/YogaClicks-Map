using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Context
{
    public interface ISecurityContext
    {
        bool Authenticated { get; }
        bool HasActor { get; }
        User CurrentUser { get; }
        Profile CurrentProfile { get; }
        IActor CurrentActor { get; }
        IList<EntityHandle> AvailableActors { get; }
        string ClientAddress { get; }
        bool CanUpdate(ISecurable securable);
        bool CanUpdateAny(params ISecurable[] securables);
        bool CanDelete(ISecurable securable);
        bool IsOwner(ISecurable securable);
        bool IsSuperUser();
        void SetCurrentUser(User user, bool persist);
        void SetCurrentActor(IActor actor);
        void RefreshProfile();
        void RefreshActors();
    }
}