using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class GroupMemberModel : EntityModel<GroupMember>
    {
        public GroupMemberModel(GroupMember entity) : base(entity) {}

        public ProfileSummaryModel Profile { get; private set; }

        public bool Administrator { get; set; }

        public bool Confirmed { get; set; }

        public override void Populate(GroupMember entity)
        {
            Profile = new ProfileSummaryModel(entity.User.Profile);
            Administrator = entity.Administrator;
            Confirmed = entity.Confirmed;
        }
    }
}