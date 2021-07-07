using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class UserStoriesModel
    {
        public UserStoriesModel(IEnumerable<UserStory> userStories)
        {
            UserStories = new List<UserStoryModel>();

            foreach (var userStory in userStories)
            {
                UserStories.Add(new UserStoryModel(userStory));
            }
        }

        public IList<UserStoryModel> UserStories { get; private set; }
    }
}