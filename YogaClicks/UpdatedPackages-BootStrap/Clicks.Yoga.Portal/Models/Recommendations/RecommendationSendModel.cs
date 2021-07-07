using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Recommendations
{
    public class RecommendationSendModel
    {
        public RecommendationSendModel()
        {
            Friends = new FriendChooserModel();
        }

        public EntityHandleModel Entity { get; private set; }

        public int EntityId { get; set; }

        public string EntityTypeName { get; set; }

        public FriendChooserModel Friends { get; set; }

        public string Group { get; set; }

        public RecommendationSendModel PopulateMetadata(IEntityHandle entity, IEnumerable<Profile> friends)
        {
            Entity = new EntityHandleModel(entity);
            Friends.PopulateMetadata(friends);
            return this;
        }
    }
}