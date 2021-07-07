using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class PrivateConversationModel : EntityModel<PrivateConversation>
    {
        public PrivateConversationModel(PrivateConversation entity) : base(entity) {}

        public override void Populate(PrivateConversation entity)
        {
        }
    }
}