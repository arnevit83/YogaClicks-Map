using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Community
{
    public class CommunityNavigationModel
    {
        public CommunityNavigationModel(IEnumerable<Group> groups, string keywords)
        {
            Groups = groups.Select(g => new NamedSummaryModel(g)).ToList();
            Keywords = keywords;
        }

        public string Keywords { get; private set; }

        public IList<NamedSummaryModel> Groups { get; private set; }
    }
}