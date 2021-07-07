using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class NewsStoryType : Entity
    {
        public const string FriendMadeFriend = "FriendMadeFriend";
        public const string FriendFanned = "FriendFanned";
        public const string FriendAddedReview = "FriendAddedReview";
        public const string FriendJoinedGroup = "FriendJoinedGroup";
        public const string FriendAttendingActivity = "FriendAttendingActivity";
        public const string FriendAddedEntity = "FriendAddedEntity";
        public const string FriendAddedPhotos = "FriendAddedPhotos";
        public const string FriendPosted = "FriendPosted";
        public const string GroupPosted = "GroupPosted";
        public const string ActivityPosted = "ActivityPosted";
        public const string FriendSharedStory = "FriendSharedStory";

        public NewsStoryType()
        {
            Stories = new List<NewsStory>();
        }

        public string Tag { get; set; }

        public string Name { get; set; }

        public bool Vain { get; set; }

        public bool Deduped { get; set; }

        public bool Shareable { get; set; }

        public TimeSpan DedupeTimeSpan
        {
            get { return new TimeSpan(0, 1, 0, 0); }
        }

        public virtual ICollection<NewsStory> Stories { get; private set; }
    }
}