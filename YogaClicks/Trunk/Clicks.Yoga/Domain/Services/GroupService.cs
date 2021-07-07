using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class GroupService : ServiceBase, IGroupService
    {
        public GroupService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityServce,
            IFriendService friendService,
            INewsService newsService,
            IRequestService requestService)
            : base(entityContext, securityContext)
        {
            EntityService = entityServce;
            FriendService = friendService;
            NewsService = newsService;
            RequestService = requestService;
        }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private INewsService NewsService { get; set; }

        private IRequestService RequestService { get; set; }

        public Group GetGroup(int id)
        {
            return EntityContext.Groups.Get(id);
        }

        public Group GetGroupForDisplay(int id)
        {
            var group = EntityContext.Groups.Get(id);
            if (group == null) throw new UnknownEntityException();
            return group;
        }

        public Group GetGroupForEditing(int id)
        {
            var permissions = GetPermissions(id);
            if (!permissions.Manage) throw new PermissionDeniedException();
            return GetGroup(id);
        }

        public Group GetFaqGroup()
        {
            return EntityContext.Groups.FirstOrDefault(e => e.Active && e.IsGlobalFaqs);
        }

        public IList<Group> GetJoinedGroups(int userId)
        {
            return EntityContext.GroupMembers
                .Where(e => e.User.Id == userId)
                .Where(e => e.Confirmed)
                .Where(e => e.Group.Active)
                .Select(e => e.Group)
                .Distinct()
                .ToList();
        }

        public IList<Group> GetProfessionalJoinedGroups(int id, string typeName)
        {
            var entity = EntityService.GetEntity<ISecurable>(id, typeName);

            if (entity == null)
                throw new UnknownEntityException();

            return EntityContext.GroupMembers
                .Where(m => m.User.Id == entity.OwnerId)
                .Where(m => m.Confirmed)
                .Where(m => m.Group.Active)
                .Where(m =>
                    !m.Group.ProfessionsRestricted ||
                    m.Group.Professions.Any(t => t.Name == typeName))
                .Select(m => m.Group)
                .ToList();
        }

        public IList<Group> GetProfessionalPromotedGroups(int id, string typeName)
        {
            return EntityContext.Groups
                .Where(g => g.PromoterHandle.EntityId == id)
                .Where(g => g.PromoterHandle.EntityType.Name == typeName)
                .Where(g => g.Active)
                .ToList();
        }

        public IList<Group> GetUnmanagedGroups(int userId)
        {
            return EntityContext.GroupMembers
                                .Where(e => e.User.Id == userId && e.Confirmed && !e.Administrator && e.Group.Active)
                                .Select(e => e.Group)
                                .Distinct()
                                .ToList();
        }

        public IList<Group> GetManagedGroups(int userId)
        {
            return EntityContext.GroupMembers
                                .Where(e => e.User.Id == userId && e.Administrator && e.Group.Active)
                                .Select(e => e.Group)
                                .Distinct()
                                .ToList();
        }

        public GroupMember GetMemberWhereExists(int groupId, int userId)
        {
            return EntityContext.GroupMembers.FirstOrDefault(e => e.Group.Id == groupId && e.User.Id == userId);
        }

        public IList<GroupMember> GetMembers(int groupId, int skip, int take)
        {
            return EntityContext.GroupMembers
                .Include(e => e.User)
                .Include(e => e.User.Profile)
                .Include(e => e.User.Profile.Image)
                .Include(e => e.User.Profile.Location)
                .Where(e => e.Group.Id == groupId)
                .Where(e => e.Confirmed)
                .Where(e => e.User.Active)
                .Where(e => e.User.Profile.Active)
                .OrderByDescending(x => x.Administrator)
                .ThenBy(x => x.User.Profile.Forename)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public int GetMembersCount(int groupId)
        {
            return EntityContext.GroupMembers
                .Count(x => x.Group.Id == groupId && x.Confirmed);
        }

        public IList<Profile> GetInvitableFriends(int groupId, int userId)
        {
            var group = EntityContext.Groups.FirstOrDefault(e => e.Id == groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var query = EntityContext.Friendships
                .Where(f => f.Profile.Owner.Id == userId && f.Confirmed)
                .Where(f => !f.Friend.Owner.Memberships.Any(m => m.Group.Id == groupId));

            if (group.Professional)
            {
                if (group.ProfessionsRestricted)
                {
                    var ids = EntityContext.Groups.Where(e => e.Id == group.Id).SelectMany(e => e.Professions).Select(e => e.Id).ToList();
                    query = query.Where(f => f.Friend.Owner.Owned.Any(e => e.Active && ids.Any(id => id == e.EntityType.Id)));
                }
                else
                {
                    query = query.Where(f => f.Friend.Owner.Owned.Any(e => e.Active && e.EntityType.IsProfessional));
                }
            }

            return query.Select(e => e.Friend).ToList();
        }

        public GroupPermissions GetPermissions(int groupId)
        {
            var authenticated = SecurityContext.Authenticated;
            var user = authenticated ? SecurityContext.CurrentUser : null;
            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var member = authenticated ? EntityContext.GroupMembers.FirstOrDefault(e => e.Group.Id == groupId && e.User.Id == user.Id && e.Confirmed) : null;
            var updatable = SecurityContext.CanUpdate(group);
            var joined = member != null;
            var administrator = member != null && member.Administrator;

            return new GroupPermissions
            {
                Access = group.Public || joined,
                Invite = updatable || administrator || (joined && group.MemberInvitationsPermitted),
                Manage = updatable || administrator,
                ManageAdministrators = updatable
            };
        }

        public GroupMembershipStatus GetMembershipStatus(int groupId)
        {
            if (!SecurityContext.Authenticated || SecurityContext.CurrentProfile.Professional)
            {
                return GroupMembershipStatus.Unknown;
            }

            var user = SecurityContext.CurrentUser;
            var membership = GetMemberWhereExists(groupId, user.Id);

            if (membership != null)
            {
                if (membership.Group.Owner.Id == user.Id)
                {
                    return GroupMembershipStatus.Owner;
                }

                return membership.Confirmed ? GroupMembershipStatus.Confirmed : GroupMembershipStatus.Unconfirmed;
            }

            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            if (group.Professional)
            {
                bool qualified;

                if (group.ProfessionsRestricted)
                {
                    var ids = EntityContext.Groups.Where(e => e.Id == group.Id).SelectMany(e => e.Professions).Select(e => e.Id).ToList();
                    qualified = EntityContext.EntityHandles.Any(h => h.Owner.Id == user.Id && ids.Any(id => id == h.EntityType.Id));
                }
                else
                {
                    qualified = EntityContext.EntityHandles.Any(h => h.Owner.Id == user.Id && h.EntityType.IsProfessional);
                }

                if (!qualified) return GroupMembershipStatus.Unqualified;
            }

            return group.Public ? GroupMembershipStatus.Joinable : GroupMembershipStatus.Requestable;
        }

        public void CreateGroup(Group group)
        {
            if (group.Owner == null)
                throw new ArgumentException("Group must have an owner.", "group");
            if (!SecurityContext.CanUpdate(group.Owner))
                throw new PermissionDeniedException();
            if (group.PromoterHandle == null)
                throw new ArgumentException("Group must have a promoter.", "group");

            group.Wall = new GroupWall();
            group.MemberInvitationsPermitted = true;
            group.MemberPostingPermitted = true;
            group.MemberGalleriesPermitted = true;

            EntityContext.Groups.Add(group);

            var member = new GroupMember
            {
                Group = group,
                User = group.Owner,
                Administrator = true,
                Confirmed = true
            };

            group.Members.Add(member);

            NewsService.PostAction(NewsStoryType.FriendAddedEntity, group.PromoterHandle, group);
        }

        public void Join(int groupId)
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var status = GetMembershipStatus(groupId);

            if (status != GroupMembershipStatus.Joinable)
                throw new PermissionDeniedException();

            if (EntityContext.GroupMembers.Any(e => e.Group.Id == groupId && e.User.Id == SecurityContext.CurrentUser.Id)) return;

            var member = new GroupMember
            {
                Group = group,
                User = SecurityContext.CurrentUser,
                Confirmed = true
            };

            NewsService.PostAction(NewsStoryType.FriendJoinedGroup, member.User.Profile, member.Group);

            EntityContext.GroupMembers.Add(member);
        }

        public void Unjoin(int groupId)
        {
            if (!SecurityContext.Authenticated)
                throw new NotAuthenticatedException();

            var member = EntityContext.GroupMembers.FirstOrDefault(e => e.Group.Id == groupId && e.User.Id == SecurityContext.CurrentUser.Id);

            if (member == null) return;

            EntityContext.GroupMembers.Remove(member);
        }

        public void RequestMembership(int groupId)
        {
            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var status = GetMembershipStatus(groupId);

            if (status != GroupMembershipStatus.Requestable)
                throw new PermissionDeniedException();

            var member = new GroupMember
            {
                Group = group,
                User = SecurityContext.CurrentUser
            };

            EntityContext.GroupMembers.Add(member);

            RequestService.Request("GroupMembershipRequest", group.Owner, SecurityContext.CurrentActor, group, null, member);
        }

        public void InviteFriend(int groupId, int userId)
        {
            InviteFriends(groupId, new[] { userId });
        }

        public void InviteFriends(int groupId, IEnumerable<int> friendIds)
        {
            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var permissions = GetPermissions(groupId);

            if (!permissions.Invite)
                throw new PermissionDeniedException();

            var friends = FriendService.GetFriends(SecurityContext.CurrentProfile.Id);

            foreach (var friendId in friendIds)
            {
                var friend = friends.FirstOrDefault(e => e.Id == friendId);

                if (friend == null) continue;
                if (EntityContext.GroupMembers.Any(e => e.Group.Id == groupId && e.User.Id == friend.Owner.Id)) continue;

                var member = new GroupMember
                {
                    Group = group,
                    User = friend.Owner
                };

                EntityContext.GroupMembers.Add(member);

                RequestService.Request("GroupInvitation", friend.Owner, SecurityContext.CurrentActor, group, null, member);
            }
        }

        public void RemoveMember(int groupId, int userId)
        {
            RemoveMembers(groupId, new[] { userId });
        }

        public void RemoveMembers(int groupId, IEnumerable<int> userIds)
        {
            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var permissions = GetPermissions(groupId);

            if (!permissions.Manage)
                throw new PermissionDeniedException();

            foreach (var userId in userIds)
            {
                var members = EntityContext.GroupMembers.Where(e => e.Group.Id == groupId && e.User.Id == userId).ToList();

                foreach (var member in members)
                {
                    if (member.User.IsOwner(group))
                        throw new PermissionDeniedException();
                    if (member.Administrator && !permissions.ManageAdministrators)
                        throw new PermissionDeniedException();

                    EntityContext.GroupMembers.Remove(member);
                }
            }
        }

        public void PromoteMember(int groupId, int userId)
        {
            PromoteMembers(groupId, new[] { userId });
        }

        public void PromoteMembers(int groupId, IEnumerable<int> userIds)
        {
            var permissions = GetPermissions(groupId);

            if (!permissions.ManageAdministrators)
                throw new PermissionDeniedException();

            foreach (var userId in userIds)
            {
                var members = EntityContext.GroupMembers.Where(e => e.Group.Id == groupId && e.User.Id == userId).ToList();

                foreach (var member in members)
                {
                    member.Administrator = true;
                }
            }
        }

        public void DemoteMember(int groupId, int userId)
        {
            DemoteMembers(groupId, new[] { userId });
        }

        public void DemoteMembers(int groupId, IEnumerable<int> userIds)
        {
            var group = GetGroup(groupId);

            if (group == null)
                throw new ArgumentOutOfRangeException("groupId");

            var permissions = GetPermissions(groupId);

            if (!permissions.ManageAdministrators)
                throw new PermissionDeniedException();

            foreach (var userId in userIds)
            {
                var members = EntityContext.GroupMembers.Where(e => e.Group.Id == groupId && e.User.Id == userId).ToList();

                foreach (var member in members)
                {
                    if (member.User.IsOwner(group))
                        throw new PermissionDeniedException();

                    member.Administrator = false;
                }
            }
        }
    }
}