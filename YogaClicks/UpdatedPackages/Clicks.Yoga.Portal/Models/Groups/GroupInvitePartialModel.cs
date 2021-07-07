using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupInvitePartialModel
    {
        public GroupInvitePartialModel(IEnumerable<Profile> friends)
        {
            Friends = new FriendChooserModel();
            Friends.PopulateMetadata(friends);
        }

        public FriendChooserModel Friends { get; private set; }
    }
}