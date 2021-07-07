using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class PasswordResetActionMapping : EntityMapping<PasswordResetAction>
    {
        public PasswordResetActionMapping()
        {
            HasRequired(e => e.Request).WithOptional().Map(a => a.MapKey("RequestId"));
            Property(e => e.ClientAddress).IsRequired().HasMaxLength(39).IsUnicode(false);
        }
    }
}