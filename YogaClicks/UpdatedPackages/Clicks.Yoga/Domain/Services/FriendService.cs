using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class FriendService : ServiceBase, IFriendService
    {
        public FriendService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IRequestService requestService,
            INewsService newsService,
            INotificationService notificationService)
            : base(entityContext, securityContext)
        {
            RequestService = requestService;
            NewsService = newsService;
            NotificationService = notificationService;
        }

        private IRequestService RequestService { get; set; }

        private INewsService NewsService { get; set; }

        private INotificationService NotificationService { get; set; }

        public int GetFriendCount(int profileId)
        {
            return EntityContext.Friendships.Count(e => e.Profile.Id == profileId && e.Confirmed && !e.BlockedByProfile && !e.BlockedByFriend);
        }

        public Profile GetFriend(int profileId, int friendId)
        {
            return EntityContext.Friendships
                .Where(e => e.Profile.Id == profileId && e.Friend.Id == friendId && e.Confirmed && !e.BlockedByProfile && !e.BlockedByFriend)
                .Select(e => e.Friend)
                .FirstOrDefault();
        }

        public IList<Profile> GetFriends(int profileId)
        {
            return EntityContext.Friendships
                .Where(e => e.Profile.Id == profileId && e.Confirmed && !e.BlockedByProfile && !e.BlockedByFriend)
                .Select(e => e.Friend)
                .ToList();
        }

        public FriendshipStatus GetFriendshipStatus(int profileId, int friendId)
        {
            var profile = EntityContext.Profiles.GetIgnoringActive(profileId);

            if (!profile.Active || profile.Professional)
            {
                return FriendshipStatus.Unknown;
            }

            var friendship = GetFriendship(profileId, friendId);

            if (friendship == null)
            {
                return FriendshipStatus.Strangers;
            }
            else
            {
                return friendship.Status;
            }
        }

        public IDictionary<int, FriendshipStatus> GetFriendshipStatuses(int profileId, IEnumerable<int> friendIds)
        {
            var statuses = EntityContext.Friendships
                .Include(f => f.Friend)
                .Where(f => f.Profile.Id == profileId)
                .Where(f => friendIds.Any(id => f.Friend.Id == id))
                .ToList()
                .ToDictionary(f => f.Friend.Id, f => f.Status);

            return friendIds.ToDictionary(
                id => id,
                id => statuses.ContainsKey(id) ? statuses[id] : FriendshipStatus.Strangers);
        }

        public IList<Friendship> GetFriendships(int profileId)
        {
            return EntityContext.Friendships.Where(e => e.Profile.Id == profileId).ToList();
        }

        public void Request(int profileId, int friendId)
        {
            var profile = EntityContext.Profiles.Get(e => e.Id == profileId);
            var friend = EntityContext.Profiles.Get(e => e.Id == friendId);

            if (profile == null)
                throw new UnknownEntityException();
            if (friend == null)
                throw new UnknownEntityException();
            if (!SecurityContext.CanUpdate(profile))
                throw new PermissionDeniedException();
            if (profile.Id == friend.Id)
                throw new InvalidOperationException("Profile cannot be their own friend.");

            var friendship = GetFriendship(profileId, friendId);

            if (friendship == null) friendship = CreateFriendship(profileId, friendId);

            if (friendship.Status != FriendshipStatus.Strangers)
                throw new FriendshipStatusException(friendship.Status);

            friendship.Pending = true;
            friendship.Inverse.Pending = true;

            RequestService.Request("FriendshipRequested", friendship.Friend.Owner, friendship.Profile, friend, null, friendship.Profile);
        }

        public void Accept(int profileId, int friendId)
        {
            ConfirmFriendship(profileId, friendId, true);
        }

        public void Reject(int profileId, int friendId)
        {
            ConfirmFriendship(profileId, friendId, false);
        }

        public void Befriend(Profile profile, Profile friend)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");
            if (friend == null)
                throw new ArgumentNullException("friend");
            if (profile.Id == friend.Id)
                throw new InvalidOperationException("Profile cannot be their own friend.");

            var friendship = CreateFriendship(profile, friend);

            friendship.Confirmed = true;
            friendship.Inverse.Confirmed = true;
        }

        public void Unfriend(int profileId, int friendId)
        {
            var friendship = GetFriendship(profileId, friendId);

            if (friendship == null)
                throw new UnknownEntityException();
            if (friendship.Status != FriendshipStatus.Friends)
                throw new FriendshipStatusException(friendship.Status);
            if (!SecurityContext.CanUpdate(friendship.Profile))
                throw new PermissionDeniedException();

            friendship.Confirmed = false;
            friendship.Inverse.Confirmed = false;
        }

        public void Block(int profileId, int friendId)
        {
            var profile = EntityContext.Profiles.Get(e => e.Id == profileId);
            var friend = EntityContext.Profiles.Get(e => e.Id == friendId);

            if (profile == null)
                throw new UnknownEntityException();
            if (friend == null)
                throw new UnknownEntityException();
            if (!SecurityContext.CanUpdate(profile))
                throw new PermissionDeniedException();

            var friendship = GetFriendship(profileId, friendId);

            if (friendship == null) friendship = CreateFriendship(profileId, friendId);

            friendship.BlockedByProfile = true;
            friendship.Inverse.BlockedByFriend = true;
        }

        public void Unblock(int profileId, int friendId)
        {
            var friendship = GetFriendship(profileId, friendId);

            if (friendship == null) return;

            if (!SecurityContext.CanUpdate(friendship.Profile))
                throw new PermissionDeniedException();
            if (!friendship.BlockedByProfile)
                throw new FriendshipStatusException(friendship.Status);

            friendship.BlockedByProfile = false;
            friendship.Inverse.BlockedByFriend = false;
        }

        private Friendship GetFriendship(int profileId, int friendId)
        {
            return EntityContext.Friendships.Get(e => e.Profile.Id == profileId && e.Friend.Id == friendId);
        }

        private Friendship CreateFriendship(int profileId, int friendId)
        {
            var profile = EntityContext.Profiles.Get(profileId);
            var friend = EntityContext.Profiles.Get(friendId);

            if (profile == null)
                throw new UnknownEntityException();
            if (friend == null)
                throw new UnknownEntityException();
            if (!SecurityContext.CanUpdate(profile))
                throw new PermissionDeniedException();
            if (profile.Id == friend.Id)
                throw new InvalidOperationException("Profile cannot be their own friend.");

            return CreateFriendship(profile, friend);
        }

        private Friendship CreateFriendship(Profile profile, Profile friend)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");
            if (friend == null)
                throw new ArgumentNullException("friend");
            if (profile.Id == friend.Id)
                throw new InvalidOperationException("Profile cannot be their own friend.");

            var inverse = new Friendship
            {
                Profile = friend,
                Friend = profile
            };

            var friendship = new Friendship
            {
                Profile = profile,
                Friend = friend,
                Inverse = inverse
            };

            EntityContext.RegisterTransientEntityDependency(inverse, e => e.Inverse, friendship);

            EntityContext.Friendships.Add(friendship);
            EntityContext.Friendships.Add(inverse);

            return friendship;
        }

        private void ConfirmFriendship(int profileId, int friendId, bool accepted)
        {
            var friendship = GetFriendship(profileId, friendId);

            if (friendship == null)
                throw new UnknownEntityException();
            if (friendship.Status != FriendshipStatus.Pending)
                throw new FriendshipStatusException(friendship.Status);
            if (!SecurityContext.CanUpdate(friendship.Profile))
                throw new PermissionDeniedException();

            if (friendship.Confirmed) return;

            friendship.Pending = false;
            friendship.Inverse.Pending = false;

            friendship.Confirmed = accepted;
            friendship.Inverse.Confirmed = accepted;

            if (accepted)
            {
                NotificationService.Notify(friendship.Friend.Owner, "FriendshipAccepted", friendship.Profile, null);
                NewsService.PostInteraction(NewsStoryType.FriendMadeFriend, friendship.Profile, friendship.Friend, null);
            }
        }
    }
}