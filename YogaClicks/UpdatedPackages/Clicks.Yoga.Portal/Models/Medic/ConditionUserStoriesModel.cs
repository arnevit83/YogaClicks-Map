using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionUserStoriesModel
    {
        public ConditionUserStoriesModel() { }

        public ConditionUserStoriesModel(Condition condition)
        {
            UserStories = new UserStoriesModel(condition.UserStories);
            Id = condition.Id;
        }

        public UserStoriesModel UserStories { get; set; }
        public int Id { get; set; }
    }
}