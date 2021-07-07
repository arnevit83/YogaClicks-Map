using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class PrivateConversationMapping : EntityMapping<PrivateConversation>
    {
        public PrivateConversationMapping()
        {
            HasMany(e => e.Participants).WithMany().Map(a => a.MapLeftKey("ConversationId").MapRightKey("HandleId").ToTable("PrivateConversationParticipant"));
        }
    }
}