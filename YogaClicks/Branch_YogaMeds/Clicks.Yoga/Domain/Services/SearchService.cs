using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class SearchService : ServiceBase, ISearchService
    {
        private readonly Dictionary<ISearchable, SearchRecord> _transientRecordCache = new Dictionary<ISearchable, SearchRecord>();

        public SearchService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            ISearchEngine searchEngine,
            IFriendService friendService)
            : base(entityContext, securityContext)
        {
            SearchEngine = searchEngine;
            FriendService = friendService;
        }

        private ISearchEngine SearchEngine { get; set; }

        private IFriendService FriendService { get; set; }

        public void PrepareCriteria(SearchCriteria criteria)
        {
            if (criteria.Location == null && SecurityContext.Authenticated)
            {
                var profile = SecurityContext.CurrentUser.Profile;

                if (profile != null && profile.Location != null)
                {
                    criteria.Location = profile.Location.Name;
                    criteria.Coordinates = profile.Location.Coordinates;

                    if (criteria.SortOrder == SearchSortOrder.Default)
                    {
                        criteria.SortOrder = SearchSortOrder.Distance;
                    }
                }
            }
            else
            {
                if (criteria.SortOrder == SearchSortOrder.Default)
                {
                    criteria.SortOrder = SearchSortOrder.Distance;
                }
            }
        }

        public SearchResponse Search(SearchCriteria criteria)
        {
            var response = SearchEngine.Search(criteria);

            if (SecurityContext.Authenticated)
            {
                var profiles = response.Results
                    .Where(r => r.EntityTypeName == typeof(Profile).Name);
                var profileIds = profiles
                    .Select(r => r.EntityId)
                    .ToList();
                var statuses = FriendService.GetFriendshipStatuses(SecurityContext.CurrentProfile.Id, profileIds);

                foreach (var profile in profiles)
                {
                    if (statuses.ContainsKey(profile.EntityId))
                    {
                        profile.FriendshipStatus = statuses[profile.EntityId];
                    }
                }
            }

            return response;
        }

        public void Index(ISearchable subject)
        {
            SynchroniseSearchRecord(subject);
        }

        public void Index(Review review)
        {
            var entityTypeId = review.SubjectHandle.EntityType.Id;
            var entityId = review.SubjectHandle.EntityId;

            var record = EntityContext.SearchRecords.Get(e => e.EntityType.Id == entityTypeId && e.EntityId == entityId);

            if (record != null)
            {
                decimal? rating = (
                    from r in EntityContext.Reviews
                    where
                        r.SubjectHandle.EntityType.Id == entityTypeId &&
                        r.SubjectHandle.EntityId == entityId &&
                        r.Active
                    select (decimal?)r.Rating).Average();

                record.Rating = rating;

                var parent = record.Parent;

                if (parent != null)
                {
                    rating = (
                        from r in EntityContext.Reviews
                        where
                            r.SubjectHandle.Parent.EntityType.Id == parent.EntityType.Id &&
                            r.SubjectHandle.Parent.EntityId == parent.EntityId &&
                            r.Active
                        select (decimal?)r.Rating).Average();

                    parent.Rating = rating;
                }
            }
        }

        public void Remove(ISearchable subject)
        {
            var record = GetRecord(subject);

            if (record != null)
            {
                foreach (var child in EntityContext.SearchRecords.Where(e => e.Parent.Id == record.Id).ToList())
                {
                    EntityContext.SearchRecords.Remove(child);
                }

                EntityContext.SearchRecords.Remove(record);
            }
        }

        public void IndexElastic(ISearchable subject)
        {
            SearchEngine.Index(subject);
        }

        private SearchRecord SynchroniseSearchRecord(ISearchable subject)
        {
            return SynchroniseSearchRecord(subject, new List<IEntity>());
        }

        private SearchRecord SynchroniseSearchRecord(ISearchable subject, ICollection<IEntity> parents)
        {
            var record = GetRecord(subject);

            if (!subject.IsSearchable)
            {
                if (record != null) Remove(subject);
                return null;
            }

            if (record == null)
            {
                record = new SearchRecord(subject)
                {
                    EntityType = EntityContext.EntityTypes.Get(subject),
                    CreationTime = DateTime.Now,
                    ModificationTime = DateTime.Now
                };

                EntityContext.SearchRecords.Add(record);

                _transientRecordCache.Add(subject, record);
            }

            if (subject.IsTransient)
            {
                EntityContext.RegisterTransientEntityDependency(record, e => e.EntityId, subject);
            }

            subject.PopulateSearchRecord(record);

            parents.Add(subject);

            var parent = subject.GetParentEntity() as ISearchable;

            if (parent != null && !parents.Contains(parent))
            {
                record.Parent = SynchroniseSearchRecord(parent, parents);
            }

            return record;
        }

        private SearchRecord GetRecord(ISearchable subject)
        {
            if (_transientRecordCache.ContainsKey(subject)) return _transientRecordCache[subject];

            if (subject.IsTransient) return null;

            var typeName = subject.GetEntityTypeName();

            return EntityContext.SearchRecords.Get(e => e.EntityType.Name == typeName && e.EntityId == subject.Id);
        }

      
    }
}