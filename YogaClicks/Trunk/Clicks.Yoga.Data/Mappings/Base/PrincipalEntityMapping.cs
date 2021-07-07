using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class PrincipalEntityMapping<TEntity> : EntityMapping<TEntity> where TEntity : PrincipalEntity
    {
        public PrincipalEntityMapping()
        {
            HasOptional(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId"));
        }
    }
}
