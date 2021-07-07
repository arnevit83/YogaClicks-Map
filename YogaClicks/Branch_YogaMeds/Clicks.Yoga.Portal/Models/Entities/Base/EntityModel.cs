using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public abstract class EntityModel<TEntity> where TEntity : class, IEntity
    {
        protected EntityModel() {}

        protected EntityModel(TEntity entity)
        {
            if (entity == null) return;

            Exists = true;
            Id = entity.Id;
            EntityTypeName = entity.GetEntityTypeName();
            CreationTime = entity.CreationTime;

            Populate(entity);
        }

        public bool Exists { get; set; }

        public int Id { get; set; }

        public string EntityTypeName { get; set; }

        public DateTime CreationTime { get; set; }

        public abstract void Populate(TEntity entity);
    }
}
