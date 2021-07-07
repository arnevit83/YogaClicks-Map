using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public interface ICommentable : IEntity
    {
        ICollection<Comment> Comments { get; }
        Type GetCommentPermissionProviderType();
        User Owner { get; }
        EntityReference? GetReferencedEntity();
    }
}
