using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Messaging
{
    public class MessagingSendModel
    {
        public MessagingSendModel()
        {
            Friends = new FriendAutocompleteModel();
        }

        public FriendAutocompleteModel Friends { get; private set; }

        public EntityHandleModel Recipient { get; set; }

        public IEnumerable<EntityReference> CombinedRecipients
        {
            get
            {
                if (Recipient != null)
                {
                    yield return new EntityReference(Recipient.EntityId, Recipient.EntityType.Name);
                }

                foreach (var id in Friends.Ids)
                {
                    yield return new EntityReference(id, typeof(Profile).Name);
                }
            }
        }

        public string Content { get; set; }

        public MessagingSendModel PopuplateMetadata(IEnumerable<Profile> friends)
        {
            Friends.PopulateMetadata(friends);
            return this;
        }
    }
}