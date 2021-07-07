using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class NewsService : ServiceBase, INewsService
    {
        public NewsService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
        }

        private readonly List<NewsStory> _storyCache = new List<NewsStory>();

        private IEntityService EntityService { get; set; }

        public bool IsSubscribed(EntityReference subject)
        {
            if (!SecurityContext.Authenticated) return false;

            var profile = SecurityContext.CurrentProfile;

            return EntityContext.NewsSubscriptions
                .Where(s => s.Subject.EntityId == subject.EntityId)
                .Where(s => s.Subject.EntityType.Name == subject.EntityTypeName)
                .Any(s => s.Subscriber.Id == profile.Id);
        }

        public IList<NewsStory> GetStories(int profileId, int? earliestStoryId, int? latestStoryId, int take, int skip)
        {
            var subjects = EntityContext.NewsSubscriptions
                .Where(s => s.Subscriber.Id == profileId)
                .Select(s => s.Subject);

            var owned = EntityContext.EntityHandles
                .Where(h => h.Owner.Id == SecurityContext.CurrentUser.Id);

            var query = EntityContext.NewsStories
                .Include(s => s.Type)
                .Include(s => s.Subject)
                .Include(s => s.Subject.Image)
                .Include(s => s.Target)
                .Include(s => s.Target.Image)
                .Include(s => s.EntityType)
                .Include(s => s.Resources)
                .Where(s => s.Actor.Active)
                .Where(s => s.Subject.Active)
                //.Where(s => s.Target.Id == null || s.Type.Vain || !owned.Contains(s.Target))
                .Where(s => (subjects.Contains(s.Subject) || s.Type.Vain && owned.Contains(s.Subject)))
                .Where(s => !subjects.Contains(s.Inverse.Subject))
                .Where(x => x.EntityType.Controller != null)
                .OrderByDescending(x => x.CreationTime)
                .Skip(skip)
                .Take(take)
                .ToList();

            if (earliestStoryId.HasValue)
            {
                query = query.Where(s => s.Id > earliestStoryId).ToList();
            }

            if (latestStoryId.HasValue)
            {
                query = query.Where(s => s.Id < latestStoryId).ToList();
            }

            return query;
        }

        public NewsStory GetStory(int id)
        {
            return EntityContext.NewsStories.Get(id);
        }

        public NewsStory GetStory(EntityReference entity)
        {
            return EntityContext.NewsStories.Get(e => e.EntityId == entity.EntityId && e.EntityType.Name == entity.EntityTypeName);
        }

        public void Subscribe(Profile profile, IEntityHandle subject)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");
            if (subject == null)
                throw new ArgumentNullException("subject");

            if (GetSubscribed(profile, subject)) return;

            var subscription = new NewsSubscription
            {
                Subscriber = profile,
                Subject = EntityService.EnsureEntityHandle(subject),
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now
            };

            EntityContext.NewsSubscriptions.Add(subscription);
        }

        public void Unsubscribe(Profile profile, IEntityHandle subject)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");
            if (subject == null)
                throw new ArgumentNullException("subject");

            var subscriptions = GetSubscription(profile, subject).ToList();

            foreach (var subscription in subscriptions)
            {
                EntityContext.NewsSubscriptions.Remove(subscription);
            }
        }

        public NewsStory PostAction(string typeTag, IEntityHandle actor, IEntityHandle target)
        {
            return PostAction(typeTag, actor, actor, target, null, null, null);
        }

        public NewsStory PostAction(string typeTag, IEntityHandle actor, IEntityHandle target, IEntity entity)
        {
            return PostAction(typeTag, actor, actor, target, entity, null, null);
        }

        public NewsStory PostAction(string typeTag, IEntityHandle subject, IEntityHandle actor, IEntityHandle target, IEntity entity, string text, IEnumerable<MediaResource> resources)
        {
            if (typeTag == null)
                throw new ArgumentNullException("typeTag");
            if (actor == null)
                throw new ArgumentNullException("actor");

            var type = EntityContext.NewsStoryTypes.FirstOrDefault(e => e.Tag == typeTag);

            if (type == null)
                throw new ArgumentOutOfRangeException("typeTag");

            var subjectHandle = EntityService.EnsureEntityHandle(subject);
            var actorHandle = actor != null ? EntityService.EnsureEntityHandle(actor) : null;
            var targetHandle = target != null ? EntityService.EnsureEntityHandle(target) : null;

            return CreateActionStory(type, subjectHandle, actorHandle, targetHandle, entity, text, resources);
        }

        public NewsStory PostInteraction(string typeTag, IEntityHandle actor1, IEntityHandle actor2)
        {
            return PostInteraction(typeTag, actor1, actor2, null);
        }

        public NewsStory PostInteraction(string typeTag, IEntityHandle actor1, IEntityHandle actor2, IEntity entity)
        {
            if (typeTag == null)
                throw new ArgumentNullException("typeTag");
            if (actor1 == null)
                throw new ArgumentNullException("actor1");
            if (actor2 == null)
                throw new ArgumentNullException("actor2");

            var type = EntityContext.NewsStoryTypes.FirstOrDefault(e => e.Tag == typeTag);

            if (type == null)
                throw new ArgumentOutOfRangeException("typeTag");

            var subject1Handle = EntityService.EnsureEntityHandle(actor1);
            var subject2Handle = EntityService.EnsureEntityHandle(actor2);

            return CreateInteractionStory(type, subject1Handle, subject2Handle, entity);
        }

        public void DeleteStories(int entityId, string entityTypeName)
        {
            DeleteStories(EntityContext.NewsStories.Where(e => e.Subject.EntityId == entityId && e.Subject.EntityType.Name == entityTypeName).ToList());
            DeleteStories(EntityContext.NewsStories.Where(e => e.Target.EntityId == entityId && e.Target.EntityType.Name == entityTypeName).ToList());
            DeleteStories(EntityContext.NewsStories.Where(e => e.EntityId == entityId && e.EntityType.Name == entityTypeName).ToList());
        }

        private void DeleteStories(IEnumerable<NewsStory> stories)
        {
            foreach (var story in stories)
            {
                foreach (var share in EntityContext.NewsStories.Where(s => s.ShareOriginal.Id == story.Id).ToList())
                {
                    EntityContext.NewsStories.Remove(share);
                }

                foreach (var post in EntityContext.WallPosts.Where(p => p.SharedStory.Id == story.Id).ToList())
                {
                    EntityContext.WallPosts.Remove(post);
                }

                EntityContext.NewsStories.Remove(story);
            }
        }

        private bool GetSubscribed(Profile subscriber, IEntityHandle subject)
        {
            return EntityContext.NewsSubscriptions.Any(s => s.Subscriber.Id == subscriber.Id && s.Subject.EntityId == subject.Id);
        }

        private IEnumerable<NewsSubscription> GetSubscription(Profile subscriber, IEntityHandle subject)
        {
            return EntityContext.NewsSubscriptions.Where(s => s.Subscriber.Id == subscriber.Id && s.Subject.EntityId == subject.Id);
        }

        private NewsStory CreateActionStory(NewsStoryType type, EntityHandle subject, EntityHandle actor, EntityHandle target, IEntity entity, string text, IEnumerable<MediaResource> resources)
        {
            if (type.Deduped && target != null)
            {
                var subjectId = subject.EntityId;
                var subjectTypeId = subject.EntityType.Id;
                var targetId = target.EntityId;
                var targetTypeId = target.EntityType.Id;
                var cutoffTime = DateTime.Now.Subtract(type.DedupeTimeSpan);

                Expression<Func<NewsStory, bool>> expression = s =>
                    s.Subject.EntityId == subjectId &&
                    s.Subject.EntityType.Id == subjectTypeId &&
                    s.Target.EntityId == targetId &&
                    s.Target.EntityType.Id == targetTypeId &&
                    s.CreationTime > cutoffTime;

                var dupe = EntityContext.NewsStories.FirstOrDefault(expression);

                if (dupe == null) dupe = _storyCache.FirstOrDefault(expression.Compile());

                if (dupe != null)
                {
                    if (resources != null)
                    {
                        foreach (var resource in resources)
                        {
                            dupe.Resources.Add(resource);
                        }
                    }

                    dupe.ModificationTime = DateTime.Now;

                    return null;
                }
            }

            var story = new NewsStory
            {
                Type = type,
                Subject = subject,
                Actor = actor,
                Target = target,
                Text = text,
                CreationTime = DateTime.Now
            };

            if (resources != null)
            {
                foreach (var resource in resources)
                {
                    story.Resources.Add(resource);
                }
            }

            EntityContext.NewsStories.Add(story);

            _storyCache.Add(story);

            if (entity != null)
            {
                story.EntityType = EntityService.GetEntityType(entity.GetEntityTypeName());

                EntityContext.RegisterTransientEntityDependency(story, e => e.EntityId, entity);
            }

            return story;
        }

        private NewsStory CreateInteractionStory(NewsStoryType type, EntityHandle actor1, EntityHandle actor2, IEntity entity)
        {
            var story = CreateActionStory(type, actor1, actor1, actor2, entity, null, null);

            if (story == null) return null;

            var inverse = CreateActionStory(type, actor2, actor2, actor1, entity, null, null);

            EntityContext.NewsStories.Add(story);
            EntityContext.NewsStories.Add(inverse);

            EntityContext.RegisterTransientEntityDependency(inverse, e => e.Inverse, story);

            return story;
        }
    }
}