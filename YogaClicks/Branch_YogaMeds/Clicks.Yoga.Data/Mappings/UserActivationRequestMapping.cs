using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class UserActivationRequestMapping : EntityMapping<UserActivationRequest>
    {
        public UserActivationRequestMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            Property(e => e.Key).IsRequired().HasMaxLength(100).IsUnicode(false);
            Property(e => e.Completed).IsRequired();
            Property(e => e.CompletionTime);
        }
    }
}