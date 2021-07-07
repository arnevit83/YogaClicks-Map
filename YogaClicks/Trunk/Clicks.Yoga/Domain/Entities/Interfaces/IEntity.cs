using System;

namespace Clicks.Yoga.Domain.Entities
{
    public interface IEntity
    {
        int Id { get; }
        bool IsTransient { get; }
        DateTime CreationTime { get; }
        string GetEntityTypeName();
        EntityReference GetEntityReference();
        IEntity GetParentEntity();
    }
}
