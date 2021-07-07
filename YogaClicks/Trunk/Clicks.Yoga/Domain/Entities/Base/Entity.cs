using System;

namespace Clicks.Yoga.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModificationTime { get; set; }

        public bool IsTransient
        {
            get { return Id == 0; }
        }

        public Type GetEntityType()
        {
            var type = GetType();
            if (!type.Namespace.StartsWith("Clicks.")) type = type.BaseType;
            return type;
        }

        public string GetEntityTypeName()
        {
            return GetEntityType().Name;
        }

        public EntityReference GetEntityReference()
        {
            return new EntityReference(Id, GetEntityTypeName());
        }

        public virtual IEntity GetParentEntity()
        {
            return null;
        }
    }
}