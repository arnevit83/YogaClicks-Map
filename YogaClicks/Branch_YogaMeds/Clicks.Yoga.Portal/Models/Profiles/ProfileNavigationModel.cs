using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileNavigationModel
    {
        public ProfileNavigationModel()
        {
            Pages = new List<EntityHandleModel>();
            Groups = new List<NamedSummaryModel<Group>>();
        }

        public ProfileNavigationModel(
            Profile profile,
            IEnumerable<EntityHandle> handles,
            IEnumerable<Group> groups) : this()
        {
            Profile = new ProfileModel(profile);

            var listed = handles.ToList();

            foreach (var handle in listed)
            {
                Pages.Add(new EntityHandleModel(handle));
            }

            foreach (var group in groups)
            {
                Groups.Add(new NamedSummaryModel<Group>(group));
            }

            CanCreateActivity = listed.Any(e => e.EntityType.IsProfessional);
        }

        public ProfileModel Profile { get; private set; }

        public IList<EntityHandleModel> Pages { get; private set; }

        public IList<NamedSummaryModel<Group>> Groups { get; private set; }

        public bool CanCreateActivity { get; private set; }
    }
}