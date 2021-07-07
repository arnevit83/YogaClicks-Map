using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupDisplayModel
    {
        public GroupDisplayModel(Group group, GroupPermissions permissions, Profile profile)
        {
            Group = new GroupModel(group);
            Permissions = permissions;
            Profile = new ProfileModel(profile);

            Styles = new List<NamedSummaryModel>();
            Professions = new List<NamedSummaryModel>();

            foreach (var style in group.Styles)
            {
                Styles.Add(new NamedSummaryModel(style));
            }

            foreach (var type in group.Professions)
            {
                Professions.Add(new NamedSummaryModel(type));
            }
        }

        public GroupModel Group { get; private set; }

        public ProfileModel Profile { get; private set; }

        public GroupPermissions Permissions { get; private set; }

        public IList<NamedSummaryModel> Styles { get; private set; }

        public IList<NamedSummaryModel> Professions { get; private set; }
    }
}