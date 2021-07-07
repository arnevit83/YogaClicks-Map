using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class PrivateMessageDeliveryMapping : EntityMapping<PrivateMessageDelivery>
    {
        public PrivateMessageDeliveryMapping()
        {
            HasRequired(e => e.Recipient).WithMany().Map(a => a.MapKey("RecipientHandleId")).WillCascadeOnDelete(false);
            HasRequired(e => e.Message).WithMany(e => e.Deliveries).Map(a => a.MapKey("MessageId")).WillCascadeOnDelete(false);
            Property(e => e.Read).IsRequired();
            Property(e => e.Hidden).IsRequired();
            Property(e => e.ReadTime);
        }
    }
}