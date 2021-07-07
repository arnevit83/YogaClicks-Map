using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IGroupService
    {
        Group GetGroup(int id);
        Group GetGroupForDisplay(int id);
        Group GetGroupForEditing(int id);
        Group GetFaqGroup();
        IList<Group> GetJoinedGroups(int userId);
        IList<Group> GetProfessionalJoinedGroups(int id, string typeName);
        IList<Group> GetProfessionalPromotedGroups(int id, string typeName);
        IList<Group> GetUnmanagedGroups(int userId);
        IList<Group> GetManagedGroups(int userId);
        GroupMember GetMemberWhereExists(int groupId, int userId);
        IList<GroupMember> GetMembers(int groupId, int skip, int take);
        int GetMembersCount(int groupId);
        IList<Profile> GetInvitableFriends(int groupId, int userId);
        GroupPermissions GetPermissions(int groupId);
        GroupMembershipStatus GetMembershipStatus(int groupId);
        void CreateGroup(Group group);
        void Join(int groupId);
        void Unjoin(int groupId);
        void RequestMembership(int groupId);
        void InviteFriend(int groupId, int userId);
        void InviteFriends(int groupId, IEnumerable<int> friendIds);
        void RemoveMember(int groupId, int userId);
        void RemoveMembers(int groupId, IEnumerable<int> userIds);
        void PromoteMember(int groupId, int userId);
        void PromoteMembers(int groupId, IEnumerable<int> userIds);
        void DemoteMember(int groupId, int userId);
        void DemoteMembers(int groupId, IEnumerable<int> userIds);
    }
}