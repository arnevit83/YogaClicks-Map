using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Groups
{
    [DataContract]
    public class GroupCreateStep3Model
    {
        public GroupCreateStep3Model()
        {
            Friends = new FriendAutocompleteModel();
        }

        [DataMember]
        public FriendAutocompleteModel Friends { get; private set; }

        public bool Back { get; set; }

        public GroupCreateModel CreateModel { get; set; }

        public void PopulateEntity(Group entity, Profile profile, IFriendService friendService)
        {
            var friends = new List<Profile>();

            Friends.PopulateCollection(profile, friends, friendService);

            foreach (var friend in friends)
            {
                entity.Members.Add(new GroupMember(entity, friend.Owner, true, true));
            }
        }

        public GroupCreateStep3Model PopulateMetadata(Profile profile, IFriendService friendService)
        {
            Friends.PopulateMetadata(friendService.GetFriends(profile.Id));
            return this;
        }
    }
}