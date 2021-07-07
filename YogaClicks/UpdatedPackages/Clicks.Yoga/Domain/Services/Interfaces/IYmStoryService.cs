using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IYmStoryService
    {
        YmStory GetStory(int id);
        
        IList<YmStory> GetStories();

        IList<YmStoryLifeChallenge> GetLifeChallenges();
        YmStoryLifeChallenge GetLifeChallenge(int id);

        IList<YmStory> GetMapStoriesBucket(int bucket);

        IList<YmStory> GetMapStoriesConditions();
    
        IList<YmStory> SearchStories(String TS,int? conditionId);

        IList<YmStory> GetStories(int ownerId, string TS);

        IList<YmStory> GetStoriesFav(int ownerId, string entityTypeName, string proType);

        void AddStory(YmStory story);

        void RemoveStory(YmStory story);

        bool HasStory(int profileId);


   
    }
}