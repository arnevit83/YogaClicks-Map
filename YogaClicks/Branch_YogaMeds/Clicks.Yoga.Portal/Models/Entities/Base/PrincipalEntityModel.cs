using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public abstract class PrincipalEntityModel<TEntity> : EntityModel<TEntity>, ISecurable where TEntity : PrincipalEntity
    {
        public PrincipalEntityModel() { }
        
        public PrincipalEntityModel(TEntity entity) : base(entity)
        {
            if (!Exists) return;
            OwnerId = entity.Owner == null ? null : (int?)entity.Owner.Id;
            Active = entity.Active;
            Populate(entity);
        }

        public int? OwnerId { get; set; }

        public bool Active { get; set; }

        public bool IsOwnedBy(int? userId)
        {
            return OwnerId == userId;
        }
    }
}