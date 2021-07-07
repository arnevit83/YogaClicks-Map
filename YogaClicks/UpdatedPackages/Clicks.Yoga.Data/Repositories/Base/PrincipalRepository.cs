using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Repositories
{
    public class PrincipalRepository<T> : Repository<T> where T : PrincipalEntity
    {
        public PrincipalRepository(YogaDataContext context) : base(context) {}

        public override IQueryable<T> Queryable
        {
            get
            {
                return base.Queryable.Where(e => e.Active);
            }
        }

        public override T Get(int id)
        {
            var entity = base.Get(id);

            if (entity != null && !entity.Active) return null;

            return entity;
        }

        public override void Remove(T entity)
        {
            entity.Active = false;
        }
    }
}