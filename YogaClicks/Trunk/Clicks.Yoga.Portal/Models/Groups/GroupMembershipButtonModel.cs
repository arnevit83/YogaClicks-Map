using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupMembershipButtonModel
    {
        public GroupMembershipButtonModel(GroupMembershipStatus status)
        {
            Status = status;
        }

        public GroupMembershipStatus Status { get; private set; }
    }
}