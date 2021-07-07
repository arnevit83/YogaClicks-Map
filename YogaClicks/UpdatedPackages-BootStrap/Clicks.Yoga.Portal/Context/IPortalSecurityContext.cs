using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Context;

namespace Clicks.Yoga.Portal.Context
{
    public interface IPortalSecurityContext
    {
        bool Authenticated { get; }
        bool HasActor { get; }
        CurrentProfileModel CurrentProfile { get; }
        CurrentActorModel CurrentActor { get; }
        IList<AvailableActorModel> AvailableActors { get; }
        bool CanUpdate(ISecurable securable);
        bool CanUpdateAny(params ISecurable[] securables);
        bool CanDelete(ISecurable securable);
        bool IsOwner(ISecurable securable);
        bool IsSuperUser();
    }
}