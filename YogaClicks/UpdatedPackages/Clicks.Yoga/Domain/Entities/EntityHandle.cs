using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class EntityHandle : Entity, IEntityHandle, IFormattable, INamed, ISecurable
    {
        public EntityHandle()
        {
            Fans = new List<Fan>();
        }

        public int EntityId { get; set; }

        public virtual EntityType EntityType { get; set; }

        public virtual Image Image { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public virtual User Owner { get; set; }

        public bool Active { get; set; }

        public virtual EntityHandle Parent { get; set; }

        public virtual ICollection<Fan> Fans { get; private set; }

        public void Update(IEntityHandle entity)
        {
            Name = entity.Name;
            Image = entity.Image;
            Location = entity.Location;
            Owner = entity.Owner;
            Active = entity.Active;
        }

        int IEntity.Id
        {
            get { return EntityId; }
        }

        string IEntity.GetEntityTypeName()
        {
            return EntityType.Name;
        }

        IEntity IEntity.GetParentEntity()
        {
            return Parent;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "N":
                    return Name;
                case "T":
                    return EntityType.DisplayName;
                case "t":
                    return EntityType.DisplayName.ToLower();
                case "U":
                    return string.Format("/{0}/{1}/{2}", EntityType.Controller, EntityId, "Display").ToLower();
                default:
                    return Name;
            }
        }

        int? ISecurable.OwnerId
        {
            get { return Owner != null ? Owner.Id : (int?)null; }
        }
    }
}