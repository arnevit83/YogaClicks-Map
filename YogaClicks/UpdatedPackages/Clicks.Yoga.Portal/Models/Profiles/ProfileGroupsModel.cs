using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System.Linq;
using System.Configuration;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileGroupsModel
    {
        public ProfileGroupsModel(Profile profile, IEnumerable<Group> joinedGroups, IEnumerable<Group> managedGroups)
        {
            JoinedGroups = new List<GroupModel>();
            foreach (var group in joinedGroups.Where(x => x.Id != int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.NewsUpdateGroup.Id"])))
            {
                JoinedGroups.Add(new GroupModel(group));
            }

            ManagedGroups = new List<GroupModel>();
            foreach (var group in managedGroups)
            {
                ManagedGroups.Add(new GroupModel(group));
            }

            Profile = new ProfileModel(profile);
        }

        public ProfileModel Profile { get; private set; }

        public IList<GroupModel> JoinedGroups { get; private set; }

        public IList<GroupModel> ManagedGroups { get; private set; }
    }
}