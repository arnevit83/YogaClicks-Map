using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupProfessionalPartialModel
    {
        public GroupProfessionalPartialModel(IEntityHandle professional, IEnumerable<Group> promotedGroups, IEnumerable<Group> joinedGroups)
        {
            Professional = new EntityHandleModel(professional);
            PromotedGroups = new List<GroupModel>(promotedGroups.Select(g => new GroupModel(g)));
            JoinedGroups = new List<GroupModel>(joinedGroups.Select(g => new GroupModel(g)));
        }

        public EntityHandleModel Professional { get; private set; }

        public IList<GroupModel> PromotedGroups { get; private set; }

        public IList<GroupModel> JoinedGroups { get; private set; }
    }
}