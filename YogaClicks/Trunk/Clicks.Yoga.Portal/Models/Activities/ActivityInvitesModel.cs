using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityInvitesModel
    {
        public ActivityInvitesModel()
        {
            Friends = new FriendChooserModel();
        }

        public ActivityInvitesModel(Activity activity) : this()
        {
            Activity = new ActivityModel(activity);
        }

        public ActivityModel Activity { get; private set; }

        public FriendChooserModel Friends { get; private set; }

        public ActivityInvitesModel PopulateMetadata(IEnumerable<Profile> friends)
        {
            Friends.PopulateMetadata(friends);
            return this;
        }
    }
}