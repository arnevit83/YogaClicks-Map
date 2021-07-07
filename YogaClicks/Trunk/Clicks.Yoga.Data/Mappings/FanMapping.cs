using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class FanMapping : EntityMapping<Fan>
    {
        public FanMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            HasRequired(e => e.EntityHandle).WithMany(e => e.Fans).Map(a => a.MapKey("EntityHandleId"));
        }
    }
}