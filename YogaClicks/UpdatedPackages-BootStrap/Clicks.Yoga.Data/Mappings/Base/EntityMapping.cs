using System.Data.Entity.ModelConfiguration;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class EntityMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public EntityMapping()
        {
            MapKey();
            Property(e => e.CreationTime).IsRequired();
            Property(e => e.ModificationTime).IsRequired();
        }

        public virtual void MapKey()
        {
            HasKey(e => e.Id);
        }
    }
}