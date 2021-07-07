using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using Newtonsoft.Json;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class FriendAutocompleteModel : MultipleEntitySelectorModel<Profile, ProfileSummaryModel>
    {
        public FriendAutocompleteModel() {}

        public FriendAutocompleteModel(IEnumerable<Profile> entities) : base(entities) {}

        public string OptionsJson
        {
            get { return JsonConvert.SerializeObject(Options); }
        }

        public string SelectedJson
        {
            get { return JsonConvert.SerializeObject(SelectedEntities); }
        }

        public void PopulateCollection(Profile profile, ICollection<Profile> collection, IFriendService service)
        {
            PopulateCollection(collection, id => service.GetFriend(profile.Id, id));
        }
    }
}