using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class InvitationMapping : EntityMapping<Invitation>
    {
        public InvitationMapping()
        {
            HasRequired(e => e.Sender).WithMany().Map(a => a.MapKey("UserId"));
            HasOptional(e => e.LoginProvider).WithMany().Map(a => a.MapKey("LoginProviderId"));
            Property(e => e.RequestIdentifier).HasMaxLength(255);
            Property(e => e.RecipientIdentifier).IsRequired().HasMaxLength(255);
        }
    }
}