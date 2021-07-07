using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using Newtonsoft.Json;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class FriendChooserModel : MultipleEntitySelectorModel<Profile, ProfileSummaryModel>
    {
        public FriendChooserModel() { }

        public string OptionsJson
        {
            get { return JsonConvert.SerializeObject(Options); }
        }

        public void PopulateCollection(Profile profile, ICollection<Profile> collection, IFriendService service)
        {
            PopulateCollection(collection, id => service.GetFriend(profile.Id, id));
        }
    }
}