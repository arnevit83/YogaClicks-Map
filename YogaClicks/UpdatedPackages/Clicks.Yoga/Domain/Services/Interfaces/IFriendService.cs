using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IFriendService
    {
        int GetFriendCount(int profileId);
        Profile GetFriend(int profileId, int friendId);
        IList<Profile> GetFriends(int profileId);
        FriendshipStatus GetFriendshipStatus(int profileId, int friendId);
        IDictionary<int, FriendshipStatus> GetFriendshipStatuses(int profileId, IEnumerable<int> friendIds);
        IList<Friendship> GetFriendships(int profileId);
        void Request(int profileId, int friendId);
        void Accept(int profileId, int friendId);
        void Reject(int profileId, int friendId);
        void Befriend(Profile profile, Profile friend);
        void Unfriend(int profileId, int friendId);
        void Block(int profileId, int friendId);
        void Unblock(int profileId, int friendId);
    }
}