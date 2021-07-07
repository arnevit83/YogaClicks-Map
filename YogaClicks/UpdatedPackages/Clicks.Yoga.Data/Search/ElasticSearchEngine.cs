using System.Configuration;
using Clicks.Yoga.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Geography;


namespace Clicks.Yoga.Data.Search
{
    public class ElasticSearchEngine : ISearchEngine
    {
        private static bool isFuzzySearch = false;

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
                if (isFuzzySearch)
                {
                    q.Query(sq => sq
                           .Bool(BoolFilterFuzzy)
                           );
                }
                else
                {

                    q.Query(sq => sq
                           .Bool(BoolFilter)
                           );
                }
            }

            q.Filter(a => a.And(AndFilter(a).ToArray()));
        }

        private IEnumerable<BaseFilter> AndFilter(FilterDescriptor<SearchRecord> q)
        {
            var baseFilters = new List<BaseFilter>();

            if (Criteria.EntityTypeIds.Count > 0)
            {
                //if Criteria.EntityTypeIds.Select(id => id.ToString() == 72 then add teacher placement.
                if (Criteria.EntityTypeIds.Any(id => id == 72) && Criteria.EntityTypeIds.GetType() != typeof(Array))
                {
                    try
                    {
                        Criteria.EntityTypeIds.Add(74);
                    }
                    catch (Exception)
                    {
                    }
                }

                baseFilters.Add(q.Terms(t => t.EntityType.Id, Criteria.EntityTypeIds.Select(id => id.ToString())));
            }

            if (Criteria.Stubbed.HasValue)
            {
                baseFilters.Add(q.Term(r => r.Stubbed, Criteria.Stubbed.Value));
            }
            if (Criteria.IsAccredited.HasValue)
            {
                if (Criteria.IsAccredited == true)
                {  
                    baseFilters.Add(q.Term(r => r.IsAccreditedTherapist, Criteria.IsAccredited.Value));
                }
             
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
            //Possibly will need a AND added to spaces, seems to be ok without with my tests
            //var andFilter = Criteria.Keywords.ToLower().Replace(" ", " AND ");

            var quoteFilter = "\"" + Criteria.Keywords.ToLower() + "\"";
            var quoteNoFilter = Criteria.Keywords.ToLower();

            b.Should(sh => sh
                           .QueryString(t => t
                               .OnFields(new[] { "name.name_analyzed", "city", "area", "country", "description" })
                               .Query(quoteFilter)
                               ));
        }

        private void BoolFilterFuzzy(BoolQueryDescriptor<SearchRecord> b)
        {
            //Possibly will need a AND added to spaces, seems to be ok without with my tests
            //var andFilter = Criteria.Keywords.ToLower().Replace(" ", " AND ");

            var quoteFilter = "\"" + Criteria.Keywords.ToLower() + "\"";
            var quoteNoFilter = Criteria.Keywords.ToLower();

            b.Should(sh => sh
                           .QueryString(t => t
                               .OnFields(new[] { "name.name_analyzed", "city", "area", "country", "description" })
                               .Query(quoteFilter)
                               .PhraseSlop(1)
                               )
                             , sh => sh
                            .Prefix("name.name_analyzed", quoteFilter)
                            , sh => sh
                            .FuzzyLikeThis(fz => fz
                                .OnFields(new[] { "name.name_analyzed", "description" })
                                .LikeText(quoteFilter)
                                .PrefixLength(1)
                                .IgnoreTermFrequency(true)

                                )
                               );
        }

        public SearchResponse Search(SearchCriteria criteria)
        {
            Criteria = criteria;

            var realPageSize = (criteria.PageSize ?? 20);

            if (realPageSize > 200)
            {
                realPageSize = 200;
            }


            var skip = criteria.PageIndex * realPageSize;

            isFuzzySearch = false;
            var result = elasticClient.Search<SearchRecord>(s =>
            {
                // if we are in a find search (non global) then limit to a 300 mile radius
                if (criteria.EntityTypeIds.Any() && !criteria.EntityTypeIds.Any(x => x.Equals(14)) && Criteria.Coordinates != null)
                {
                    if (Criteria.SortOrder == SearchSortOrder.Distance && Criteria.Coordinates != null)
                    {
                        s.SortGeoDistance(d => d
                            .OnField(f => f.Location)

                            .PinTo(Criteria.Coordinates.Latitude, Criteria.Coordinates.Longitude)
                            .Unit(GeoUnit.mi)
                            .Ascending());
                    }



     




                    s.Index(ConfigurationManager.AppSettings["ElasticSearchIndex"])
                        .Types("searchrecords")
                        .Explain()
                        .Query(q => q.Filtered(Filter))
                        //filters based on a 200 mile radius around the search zone
                        .Filter(filter => filter.GeoDistance(record => record.Location,
                            descriptor => descriptor.Distance(400.00, GeoUnit.mi)
                            .Location(Criteria.Coordinates.Latitude, Criteria.Coordinates.Longitude)
                            ))
                        .Skip(skip)
                        .Sort(f => f.OnField(x => x.ImageGuid == null))
                        .Take(realPageSize);
                    }
                

                else
                {
                    if (Criteria.SortOrder == SearchSortOrder.Distance && Criteria.Coordinates != null)
                    {
                        s.SortGeoDistance(d => d
                            .OnField(f => f.Location)

                            .PinTo(Criteria.Coordinates.Latitude, Criteria.Coordinates.Longitude)
                            .Unit(GeoUnit.mi)
                            .Ascending());
                    }




                    s.Index(ConfigurationManager.AppSettings["ElasticSearchIndex"])
                        .Types("searchrecords")
                        .Explain()
                        .Query(q => q.Filtered(Filter))
                        .Sort(f => f.OnField(x => x.ImageGuid == null))
                        .Skip(skip)
                        .Take(realPageSize);
                }

            

                return s;
            });


            var resultList = result.Documents.ToList();
            var hitCount = result.Total;

            if (resultList.Count == 0 && !string.IsNullOrWhiteSpace(Criteria.Keywords))
            {
                isFuzzySearch = true;
                var fuzzyresult = elasticClient.Search<SearchRecord>(s =>
                {
                    s.Index(ConfigurationManager.AppSettings["ElasticSearchIndex"])
                        .Types("searchrecords")
                        .Explain()
                        .Query(q => q.Filtered(Filter))
                        .Skip(skip)
                      
                        .Take(realPageSize);
                    return s;
                });

                var resultstoexclude = fuzzyresult.DocumentsWithMetaData.Where(x => x.Score < 0.04 && x.Score != 0 ).ToList();
                resultList = fuzzyresult.Documents.ToList();
                resultList.RemoveAll(x => resultstoexclude.Any(y => y.Source.Name == x.Name));




                hitCount = fuzzyresult.DocumentsWithMetaData.Count() - resultstoexclude.Count();
            }











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
                if (Criteria.SortOrder == SearchSortOrder.Distance && Criteria.Coordinates != null)
                {
                    if (item.Location != null)
                    {
                        search.Distance = distance(item.Location.Latitude, item.Location.Longitude,
                        criteria.Coordinates.Latitude, criteria.Coordinates.Longitude, 'M');
                    }
                }
                search.LocationGeography = item.LocationGeography;
                search.Postcode = item.Postcode;
                search.Rating = item.Rating;
                search.Stubbed = item.Stubbed;
                search.ImageGuid = item.ImageGuid;
                search.ImageExtension = item.ImageExtension;
                search.ImagePath = item.ImagePath;

                if (item.EntityType.Name == "TeacherPlacement")
                {
                    search.ParentEntityId = item.OwnerId;
                }
               
                    search.IsAccreditedTherapist = item.IsAccreditedTherapist;
            


               

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

            totalCount = hitCount;
            
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

            var sortedList = new List<SearchResult>();

            if (Criteria.SortOrder == SearchSortOrder.Distance && Criteria.Coordinates != null)
            {
                sortedList = results.OrderBy(x => x.Distance).ToList();
            }
            else if (Criteria.SortOrder == SearchSortOrder.Name)
            {
                sortedList = results.OrderBy(x => x.Name).ToList();
            }
            else
            {
                //Order of Relivance (how it comes out of elastic search
                return new SearchResponse(results, totalCount, hasNextPage, hasPreviousPage,isFuzzySearch);
            }

            return new SearchResponse(sortedList, totalCount, hasNextPage, hasPreviousPage, isFuzzySearch);

        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles (default)                         :::
        //:::                  'K' is kilometers                                      :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::
        //:::  Worldwide cities and other features databases with latitude longitude  :::
        //:::  are available at http://www.geodatasource.com                          :::
        //:::                                                                         :::
        //:::  For enquiries, please contact sales@geodatasource.com                  :::
        //:::                                                                         :::
        //:::  Official Web site: http://www.geodatasource.com                        :::
        //:::                                                                         :::
        //:::           GeoDataSource.com (C) All Rights Reserved 2015                :::
        //:::                                                                         :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
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
