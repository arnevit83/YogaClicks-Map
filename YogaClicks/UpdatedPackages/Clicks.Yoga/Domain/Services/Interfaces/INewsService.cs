using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface INewsService
    {
        bool IsSubscribed(EntityReference subject);
        IList<NewsStory> GetStories(int profileId, int? earliestStoryId, int? latestStoryId, int take, int skip);
        NewsStory GetStory(int id);
        NewsStory GetStory(EntityReference entity);
        void Subscribe(Profile profile, IEntityHandle subject);
        void Unsubscribe(Profile profile, IEntityHandle subject);
        NewsStory PostAction(string typeTag, IEntityHandle actor, IEntityHandle target);
        NewsStory PostAction(string typeTag, IEntityHandle actor, IEntityHandle target, IEntity entity);
        NewsStory PostAction(string typeTag, IEntityHandle subject, IEntityHandle actor, IEntityHandle target, IEntity entity, string text, IEnumerable<MediaResource> resources);
        NewsStory PostInteraction(string typeTag, IEntityHandle actor1, IEntityHandle actor2);
        NewsStory PostInteraction(string typeTag, IEntityHandle actor1, IEntityHandle actor2, IEntity entity);
        void DeleteStories(int entityId, string entityTypeName);
    }
}