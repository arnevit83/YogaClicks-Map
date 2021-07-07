using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Repositories
{
    public interface IEntityTypeRepository : IRepository<EntityType>
    {
        EntityType Get(IEntity entity);
    }
}