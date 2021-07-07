using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class PrivateMessageMapping : EntityMapping<PrivateMessage>
    {
        public PrivateMessageMapping()
        {
            HasRequired(e => e.Conversation).WithMany(e => e.Messages).Map(a => a.MapKey("ConversationId"));
            HasRequired(e => e.Sender).WithMany().Map(a => a.MapKey("SenderHandleId"));
            Property(e => e.Content).IsRequired();
        }
    }
}