using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;

namespace Clicks.Yoga.Data.Repositories
{
    public class EntityTypeRepository : Repository<EntityType>, IEntityTypeRepository
    {
        public EntityTypeRepository(YogaDataContext context) : base(context) { }

        public EntityType Get(IEntity entity)
        {
            var name = entity.GetEntityTypeName();
            return Get(e => e.Name == name);
        }
    }
}