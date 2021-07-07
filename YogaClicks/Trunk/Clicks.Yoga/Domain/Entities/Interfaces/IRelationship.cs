using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public interface IRelationship
    {
        IEnumerable<Entity> Targets { get; }
    }
}