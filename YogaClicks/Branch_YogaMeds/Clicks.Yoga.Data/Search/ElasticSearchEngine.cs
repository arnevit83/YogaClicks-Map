using System.Configuration;
using Clicks.Yoga.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Clicks.Yoga.Domain.Entities;


namespace Clicks.Yoga.Data.Search
{
    public class ElasticSearchEngine : ISearchEngine
    {
        public ElasticSearchEngine(IEntityContext entityContext)
        {
            EntityContext = entityContext;
        }
        private IEntityContext EntityContext { get; set; }

        ElasticClient elasticClient = GetElasticClient;
        private static ElasticClient GetElasticClient
        {
            get
            {
                var localhost = new Uri(ConfigurationManager.AppSettings["ElasticSearchHost"]);
                var setting = new ConnectionSettings(localhost)
                        .SetDefaultIndex(ConfigurationManager.AppSettings["ElasticSearchIndex"]);
                return new ElasticClient(setting);
            }
        }

        private SearchCriteria Criteria { get; set; }

        private void Filter(FilteredQueryDescriptor<SearchRecord> q)
        {
            if (!string.IsNullOrWhiteSpace(Criteria.Keywords))
            {

                q.Query(sq => sq
                       .Bool(BoolFilter)
                       );
            }

            q.Filter(a => a.And(AndFilter(a).ToArray()));
        }

        private IEnumerable<BaseFilter> AndFilter(FilterDescriptor<SearchRecord> q)
        {
            var baseFilters = new List<BaseFilter>();

            if (Criteria.EntityTypeIds.Count > 0)
            {
                baseFilters.Add(q.Terms(t => t.EntityType.Id, Criteria.EntityTypeIds.Select(id => id.ToString())));
            }

            if (Criteria.Stubbed.HasValue)
            {
                baseFilters.Add(q.Term(r => r.Stubbed, Criteria.Stubbed.Value));
            }

            if (Criteria.Owned.HasValue)
            {
                baseFilters.Add(Criteria.Owned.Value ? q.Exists(r => r.OwnerId) : q.Not(s => s.Exists(r => r.OwnerId)));
            }

            if (Criteria.StyleId != null)
            {
                baseFilters.Add(q.Nested(n => n
                    .Path(p => p.LinkedEntities)
                    .Query(f => f
                            .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] { Criteria.StyleId.ToString() }),
                            t => t.Terms("linkedEntities.typeName", typeof(Style).Name))
                       ))));
            }

            if (Criteria.ConditionId != null)
            {
                baseFilters.Add(q.Nested(n => n
                    .Path(p => p.LinkedEntities)
                    .Query(f => f
                            .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] { Criteria.ConditionId.ToString() }),
                            t => t.Terms("linkedEntities.typeName", typeof(Condition).Name))
                       ))));
            }

            if (Criteria.VenueTypeId != null)
            {
                baseFilters.Add(q.Nested(n => n
                                   .Path(p => p.LinkedEntities)
                                   .Query(f => f
                                           .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] { Criteria.VenueTypeId.ToString() }),
                                           t => t.Terms("linkedEntities.typeName", typeof(VenueType).Name))
                                      ))));
            }

            if (Criteria.VenueServiceId != null)
            {
                baseFilters.Add(q.Nested(n => n
                                   .Path(p => p.LinkedEntities)
                                   .Query(f => f
                                           .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] { Criteria.VenueServiceId.ToString() }),
                                           t => t.Terms("linkedEntities.typeName", typeof(VenueService).Name))
                                      ))));
            }

            if (Criteria.TeacherServiceId != null)
            {
                baseFilters.Add(q.Nested(n => n
                                   .Path(p => p.LinkedEntities)
                                   .Query(f => f
                                           .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] { Criteria.TeacherServiceId.ToString() }),
                                           t => t.Terms("linkedEntities.typeName", typeof(TeacherService).Name))
                                      ))));
            }

            if (Criteria.AccreditationBodyId != null)
            {
                baseFilters.Add(q.Nested(n => n
                                   .Path(p => p.LinkedEntities)
                                   .Query(f => f
                                           .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] { Criteria.AccreditationBodyId.ToString() }),
                                           t => t.Terms("linkedEntities.typeName", typeof(AccreditationBody).Name))
                                      ))));
            }

            return baseFilters;
        }
        private void BoolFilter(BoolQueryDescriptor<SearchRecord> b)
        {
            b.Should(sh => sh
                           .QueryString(t => t
                               .OnFields(new[] { "name.name_analyzed", "city", "area", "country", "postcode" })
                               .PhraseSlop(2)
                               .Query(Criteria.Keywords.ToLower())),
                               sh => sh
                           .Prefix("name.name_analyzed", Criteria.Keywords.ToLower()),
                               sh => sh
                           .FuzzyLikeThis(fz => fz
                               .OnFields(new[] { "name.name_analyzed", "city", "area", "country", "postcode" })
                               .LikeText(Criteria.Keywords.ToLower())
                               .PrefixLength(1)
                               .MaxQueryTerms(25)
                               .IgnoreTermFrequency(true))
                               );
        }

        public SearchResponse Search(SearchCriteria criteria)
        {
            Criteria = criteria;

            var realPageSize = (criteria.PageSize ?? 20);
            if (realPageSize > 200)
                realPageSize = 200;
            var skip = criteria.PageIndex * realPageSize;
            
            var result = elasticClient.Search<SearchRecord>(s =>
            {
                s.Index(ConfigurationManager.AppSettings["ElasticSearchIndex"])
                    .Types("searchrecords")
                    .Explain()
                    .Query(q => q.Filtered(Filter))
                    .Skip(skip)
                    .Take(realPageSize);

                if (Criteria.SortOrder == SearchSortOrder.Distance && Criteria.Coordinates != null)
                {
                    s.SortGeoDistance(d => d
                        .OnField(f => f.Location)
                        .PinTo(Criteria.Coordinates.Latitude, Criteria.Coordinates.Longitude)
                        .Unit(GeoUnit.mi)
                        .Ascending());
                }
                else if (Criteria.SortOrder == SearchSortOrder.Name)
                {
                    s.SortAscending("name.name_notanalyzed");
                }
                else
                {
                    s.SortDescending(f => f.CreationTime);
                }

                return s;
            });

            var resultList = result.Documents.ToList();

            var results = new List<SearchResult>();

            foreach (var item in resultList)
            {
                var search = new SearchResult();

                search.Name = item.Name;
                search.Description = item.Description;
                search.EntityId = item.EntityId;
                search.Area = item.Area;
                search.City = item.City;
                search.Controller = item.EntityType.Controller;
                search.Country = item.Country;
                search.EntityTypeDisplayName = item.EntityType.DisplayName;
                search.EntityTypeDisplayPlural = item.EntityType.DisplayPlural;
                search.EntityTypeName = item.EntityType.Name;
                search.Location = item.Location;
                search.LocationGeography = item.LocationGeography;
                search.Postcode = item.Postcode;
                search.Rating = item.Rating;
                search.Stubbed = item.Stubbed;
                search.ImageGuid = item.ImageGuid;
                search.ImageExtension = item.ImageExtension;
                search.ImagePath = item.ImagePath;

                if (item.Parent != null)
                {
                    search.ParentEntityId = item.Parent.EntityId;
                    search.ParentEntityTypeName = item.Parent.EntityType.Name;
                    search.ParentName = item.Parent.Name;
                    search.ParentDescription = item.Parent.Description;
                    search.ParentCity = item.Parent.City;
                    search.ParentArea = item.Parent.Area;
                    search.ParentCountry = item.Parent.Country;
                    search.ParentPostcode = item.Parent.Postcode;
                    search.ParentLocation = item.Parent.Location;
                }

                results.Add(search);
            }

            var totalCount = 0;
            var hasNextPage = false;
            var hasPreviousPage = false;

            totalCount = result.Hits == null ? 0 : result.Hits.Total;

            if (criteria.PageSize > 0)
            {
                if (totalCount > (criteria.PageIndex + 1) * realPageSize)
                {
                    hasNextPage = true;
                }

                if (criteria.PageIndex > 0)
                {
                    hasPreviousPage = true;
                }
            }

            return new SearchResponse(results, totalCount, hasNextPage, hasPreviousPage);

        }

        public void Index(ISearchable subject)
        {
            var record = elasticClient.Get<SearchRecord>(subject.GetEntityTypeName() + subject.Id.ToString());

            if (record != null)
            {
                elasticClient.DeleteById<SearchRecord>(subject.GetEntityTypeName() + subject.Id.ToString());
            }

            if (subject.IsSearchable)
            {
                record = CreateSearchRecord(subject);

                var parent = subject.GetParentEntity() as ISearchable;

                if (parent != null)
                {
                    record.Parent = CreateSearchRecord(parent);
                }

                elasticClient.Index(record, ConfigurationManager.AppSettings["ElasticSearchIndex"], "searchrecords", subject.GetEntityTypeName() + subject.Id.ToString());

                foreach (var child in subject.GetChildSearchables())
                {
                    Index(child);
                }
            }
        }

        public void Index(Review review)
        {
            var entityTypeId = review.SubjectHandle.EntityType.Id;
            var entityId = review.SubjectHandle.EntityId;

            var record = elasticClient.Get<SearchRecord>(review.SubjectHandle.EntityType.Name + review.SubjectHandle.EntityId.ToString());

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

                elasticClient.Update<SearchRecord>(u => u
                    .Id(review.SubjectHandle.EntityType.Name + review.SubjectHandle.EntityId.ToString())
                    .Type("searchrecords")
                    .Script("ctx._source.rating = value")
                    .Params(d => d
                        .Add("value", rating))
                    .RetriesOnConflict(5)
                    .Refresh()
                    );
            }
        }

        public void Delete(ISearchable subject)
        {
            elasticClient.DeleteById<SearchRecord>(subject.GetEntityTypeName() + subject.Id.ToString());
        }

        private SearchRecord CreateSearchRecord(ISearchable subject)
        {
            var record = new SearchRecord(subject)
            {
                EntityType = EntityContext.EntityTypes.Get(subject),
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now
            };

            if (record.Image != null)
            {
                record.ImageGuid = record.Image.Guid;
                record.ImagePath = record.Image.Path;
                record.ImageExtension = record.Image.Type.Extension;
            }

            return record;
        }
    }
}
