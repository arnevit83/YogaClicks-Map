using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class PrivateConversation : Entity
    {
        public PrivateConversation()
        {
            Participants = new List<EntityHandle>();
            Messages = new List<PrivateMessage>();
        }

        public virtual ICollection<EntityHandle> Participants { get; private set; }

        public virtual ICollection<PrivateMessage> Messages { get; private set; }
    }
}