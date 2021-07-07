using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clicks.Yoga.Data;
using Clicks.Yoga.Data.Repositories;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Geography;
using Nest;
using Newtonsoft.Json;
using VenueService = Clicks.Yoga.Domain.Entities.VenueService;

namespace UpdatedSearch
{
    internal class Program
    {
        public static SearchCriteria Criteria = new SearchCriteria();

        private static ElasticClient GetElasticClient
        {
            get
            {
                var localhost = new Uri(ConfigurationManager.AppSettings["ElasticSearchHost"]);
                var setting = new ConnectionSettings(localhost)
                    .SetDefaultIndex(ConfigurationManager.AppSettings["ElasticSearchIndex"])
                    .SetMaximumAsyncConnections(20);
                return new ElasticClient(setting);
            }
        }

        private static void Main(string[] args)
        {

            Search();

            //Create();

            Console.WriteLine("Done...");

            Console.ReadKey();
        }

        public static void Search()
        {
            var results1 = GetElasticClient.Search<TeacherPlacementSearchRecord>(s => s
                .From(0)
                .Query(q => q
                    .FuzzyLikeThis(fd=>fd
                        .OnFields(record => record.Name)
                        .LikeText("simon low")
                        .PrefixLength(1)
                        .Boost(1)
                    )
                )
                );

            var termQuery = Query<TeacherPlacementSearchRecord>
            .Term(p => p.FirstName, "John");


            var termQuery2 = Query<SearchRecord>
            .Term(p => p.Name, "Simon Low");

            var results2 = GetElasticClient.Search<TeacherPlacementSearchRecord>(s => s
                .From(0)
                .Size(10)
                // Query here
                .Query(q => q.MatchAll() && termQuery)
            );

            var results3 = GetElasticClient.Search<SearchRecord>(s => s
                .From(0)
                .Size(10)
                // Query here
                .Query(q => q.MatchAll() && q.QueryString(
                    qa=>qa
                        .Query("Simon Low")
                        .DefaultOperator(Operator.Or)
                        .PhraseSlop(1)
                        .FuzzyPrefixLength(1)
                        )));


            var result = GetElasticClient.Search<TeacherPlacementSearchRecord>(body => body.Query(query => query.QueryString(qs => qs.Query("John"))));

                //.Filter(f=>f.Bool(b=>b.Should(sh => sh
                //           .Terms(t => t
                //               .OnFields(new[] { "name.name_analyzed", "city", "area", "country", "postcode" })
                //               .PhraseSlop(1)
                //               .Query(Criteria.Keywords.ToLower())),
                //               sh => sh
                //           .Prefix("name.name_analyzed", Criteria.Keywords.ToLower()),
                //               sh => sh
                //           .FuzzyLikeThis(fz => fz
                //               .OnFields(new[] { "name.name_analyzed", "city", "area", "country", "postcode" })
                //               .LikeText(Criteria.Keywords.ToLower())
                //               .PrefixLength(3)
                //               .MaxQueryTerms(25)
                //               .IgnoreTermFrequency(true))))
                //.Filter(f => f
                //    .GeoDistance(
                //        n => n.GeoLocation,
                //        d => d.Distance(2000.0, GeoUnit.Miles).Location(52.4743918, -1.896656)
                //    )
                //)

            var resulthits = results3.Hits;

            Console.WriteLine("HD");
            //var result = GetElasticClient.Search<SearchRecord>(s =>
            //{
            //    s.Index(ConfigurationManager.AppSettings["ElasticSearchIndex"])
            //        .Types("searchrecords")
            //        .Explain()
            //        .Query(q => q.Filtered(Filter))
            //        ;

            //    if (Criteria.SortOrder == SearchSortOrder.Distance && Criteria.Coordinates != null)
            //    {
            //        s.SortGeoDistance(d => d
            //            .OnField(f => f.Location)
            //            .PinTo(Criteria.Coordinates.Latitude, Criteria.Coordinates.Longitude)
            //            .Unit(GeoUnit.mi)
            //            .Ascending());
            //    }
            //    else if (Criteria.SortOrder == SearchSortOrder.Name)
            //    {
            //        s.SortAscending("name.name_notanalyzed");
            //    }
            //    else
            //    {
            //        //s.SortDescending(f => f.CreationTime);
            //    }

            //    return s;
            //});

            foreach (var doc in results1.Documents)
            {
                Console.WriteLine("Found: " + doc.Name);
            }

            Console.WriteLine("Change");

            foreach (var doc in results2.Documents)
            {
                Console.WriteLine("Found: " + doc.Name);
            }


            foreach (var doc in results3.Documents)
            {
                Console.WriteLine("Found: " + doc.Name);
            }
        }

        //public static void Search()
        //{
        //    var results = GetElasticClient.Search<TeacherPlacementSearchRecord>(s => s
        //      .Filter(f => f
        //        .GeoDistance(
        //          n => n.GeoLocation,
        //          d => d.Distance(20.0, GeoUnit.Miles).Location(52.4743918, -1.896656)
        //        )
        //      )
        //    );

        //    foreach (var doc in results.Documents)
        //    {
        //        Console.WriteLine("Found: " + doc.Id);
        //    }
        //}

        //private static void Filter(FilteredQueryDescriptor<SearchRecord> q)
        //{
        //    if (!string.IsNullOrWhiteSpace(Criteria.Keywords))
        //    {

        //        q.Query(sq => sq
        //            .Bool(BoolFilter)
        //            );
        //    }

        //    q.Filter(a => a.And(AndFilter(a).ToArray()));
        //}

        //private static void BoolFilter(BoolQueryDescriptor<SearchRecord> b)
        //{
        //    b.Should(sh => sh
        //        .QueryString(t => t
        //            .OnFields(new[] {"name.name_analyzed", "city", "area", "country", "postcode"})
        //            .PhraseSlop(1)
        //            .Query(Criteria.Keywords.ToLower())),
        //        sh => sh
        //            .Prefix("name.name_analyzed", Criteria.Keywords.ToLower()),
        //        sh => sh
        //            .FuzzyLikeThis(fz => fz
        //                .OnFields(new[] {"name.name_analyzed", "city", "area", "country", "postcode"})
        //                .LikeText(Criteria.Keywords.ToLower())
        //                .PrefixLength(3)
        //                .MaxQueryTerms(25)
        //                .IgnoreTermFrequency(true))
        //        );
        //}

        //private static IEnumerable<BaseFilter> AndFilter(FilterDescriptor<SearchRecord> q)
        //{
        //    var baseFilters = new List<BaseFilter>();

        //    if (Criteria.EntityTypeIds.Count > 0)
        //    {
        //        baseFilters.Add(q.Terms(t => t.EntityType.Id, Criteria.EntityTypeIds.Select(id => id.ToString())));
        //    }

        //    if (Criteria.Stubbed.HasValue)
        //    {
        //        baseFilters.Add(q.Term(r => r.Stubbed, Criteria.Stubbed.Value));
        //    }

        //    if (Criteria.Owned.HasValue)
        //    {
        //        baseFilters.Add(Criteria.Owned.Value ? q.Exists(r => r.OwnerId) : q.Not(s => s.Exists(r => r.OwnerId)));
        //    }

        //    if (Criteria.StyleId != null)
        //    {
        //        baseFilters.Add(q.Nested(n => n
        //            .Path(p => p.LinkedEntities)
        //            .Query(f => f
        //                .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] {Criteria.StyleId.ToString()}),
        //                    t => t.Terms("linkedEntities.typeName", typeof (Style).Name))
        //                ))));
        //    }

        //    if (Criteria.ConditionId != null)
        //    {
        //        baseFilters.Add(q.Nested(n => n
        //            .Path(p => p.LinkedEntities)
        //            .Query(f => f
        //                .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] {Criteria.ConditionId.ToString()}),
        //                    t => t.Terms("linkedEntities.typeName", typeof (Condition).Name))
        //                ))));
        //    }

        //    if (Criteria.VenueTypeId != null)
        //    {
        //        baseFilters.Add(q.Nested(n => n
        //            .Path(p => p.LinkedEntities)
        //            .Query(f => f
        //                .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] {Criteria.VenueTypeId.ToString()}),
        //                    t => t.Terms("linkedEntities.typeName", typeof (VenueType).Name))
        //                ))));
        //    }

        //    if (Criteria.VenueServiceId != null)
        //    {
        //        baseFilters.Add(q.Nested(n => n
        //            .Path(p => p.LinkedEntities)
        //            .Query(f => f
        //                .Bool(m => m.Must(t => t.Terms("linkedEntities.id", new[] {Criteria.VenueServiceId.ToString()}),
        //                    t => t.Terms("linkedEntities.typeName", typeof (VenueService).Name))
        //                ))));
        //    }

        //    if (Criteria.TeacherServiceId != null)
        //    {
        //        baseFilters.Add(q.Nested(n => n
        //            .Path(p => p.LinkedEntities)
        //            .Query(f => f
        //                .Bool(
        //                    m => m.Must(t => t.Terms("linkedEntities.id", new[] {Criteria.TeacherServiceId.ToString()}),
        //                        t => t.Terms("linkedEntities.typeName", typeof (TeacherService).Name))
        //                ))));
        //    }

        //    if (Criteria.AccreditationBodyId != null)
        //    {
        //        baseFilters.Add(q.Nested(n => n
        //            .Path(p => p.LinkedEntities)
        //            .Query(f => f
        //                .Bool(
        //                    m =>
        //                        m.Must(
        //                            t => t.Terms("linkedEntities.id", new[] {Criteria.AccreditationBodyId.ToString()}),
        //                            t => t.Terms("linkedEntities.typeName", typeof (AccreditationBody).Name))
        //                ))));
        //    }

        //    return baseFilters;
        //}

        public static void Create()
        {

            GetElasticClient.CreateIndex("psdb", ci => ci
                .AddMapping<TeacherPlacementSearchRecord>(mt => mt
                    .MapFromAttributes() // all default types will be mapped as primitives
                    .Properties(p => p
                        .String(sn => sn.Name(snn=>snn.Name))
                        //.GeoPoint(g => g.Name(n => n.GeoLocation).IndexLatLon())
                        .GeoPoint(g => g
                            .Name(n => n.GeoLocation)
                            .IndexGeoHash()
                            .IndexLatLon()
                            .GeoHashPrecision(12)
                        )
                        //.MultiField(mf => mf
                        //.Name(n => n.Name)
                        //)
                    )
                )
                );            
            
            //GetElasticClient.CreateIndex("psdb", ci => ci
            //    .AddMapping<TeacherPlacementSearchRecord>(mt => mt
            //        .MapFromAttributes() // all default types will be mapped as primitives
            //        .Properties(p => p
            //            .String(sn => sn.Name(snn=>snn.Name))
            //            //.GeoPoint(g => g.Name(n => n.GeoLocation).IndexLatLon())
            //            .GeoPoint(g => g
            //                .Name(n => n.GeoLocation)
            //                .IndexGeoHash()
            //                .IndexLatLon()
            //                .GeoHashPrecision(12)
            //            )
            //            .MultiField(mf => mf
            //            .Name(n => n.Name)
            //            .Fields(f => f
            //                .String(s => s
            //                    .Name("name_notanalyzed")
            //                    .IncludeInAll(false)
            //                    .Index(FieldIndexOption.NotAnalyzed))
            //                .String(s => s
            //                    .Name("name_analyzed")
            //                    .IncludeInAll(false)
            //                    .Index(FieldIndexOption.Analyzed))))
            //        )
            //    )
            //    );


            var dataContext = new YogaDataContext();
            var entityContext = new YogaEntityContext(dataContext);
            AutoMapperConfiguration.CreateMaps();


            entityContext.GetType()
                .GetProperty("EntityTypes")
                .SetValue(entityContext, new EntityTypeRepository(dataContext));


            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof (ISearchable).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .OrderBy(t => t.Name);

            var tpSearchRecords = new TeacherPlacementSearchRecords();

            StringBuilder sbMyStringBuilder = new StringBuilder();

            foreach (var type in types)
            {
                if (type.Name == "Teacher")
                {
                    var set = dataContext.Set(type);

                    var teacherRecords = Enumerable.Cast<Teacher>(set).ToList();

                    foreach (var record in teacherRecords)
                    {

                        if (record.Placements.Count == 0 && record.Location != null)
                        {
                            sbMyStringBuilder.AppendLine(record.Name + " - 0 Placements ");
                            var newRecord = new TeacherPlacementSearchRecord();
                            newRecord.Id = "T" + record.Id;
                            newRecord.Name = record.Name;

                            var split = newRecord.Name.Split(' ');

                            if (split.Count() == 2)
                            {
                                newRecord.FirstName = split[0];
                                newRecord.LastName= split[1];

                            }

                            var lat = record.Location.Coordinates.Latitude;
                            var lng = record.Location.Coordinates.Longitude;
                            newRecord.GeoLocation = new GeographicPoint(lat, lng);

                            tpSearchRecords.Records.Add(newRecord);
                        }
                        else
                        {
                            sbMyStringBuilder.AppendLine(record.Name + " - " + record.Placements.Count);
                            foreach (var placement in record.Placements)
                            {
                                sbMyStringBuilder.AppendLine(placement.Venue.Name);
                                var newRecord = new TeacherPlacementSearchRecord();
                                newRecord.Id = "TP" + placement.Id;
                                newRecord.Name = record.Name;
                                var lat = placement.Venue.Address.Location.Latitude;
                                var lng = placement.Venue.Address.Location.Longitude;
                                newRecord.GeoLocation = new GeographicPoint(lat, lng);

                                tpSearchRecords.Records.Add(newRecord);
                            }
                        }

                    }

                    foreach (var record in tpSearchRecords.Records)
                    {
                        GetElasticClient.Index(record);
                    }

                    //System.IO.File.WriteAllText(@"Q:\yogatest.txt", sbMyStringBuilder.ToString());
                }


            }



        }
        
        public class TeacherPlacementSearchRecord
        {
            public string Id { get; set; }

            [ElasticProperty(Type = FieldType.GeoPoint)]
            public GeographicPoint GeoLocation { get; set; }

            public string Name { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }
        }

        public class TeacherPlacementSearchRecords
        {
            public TeacherPlacementSearchRecords()
            {
                this.Records = new List<TeacherPlacementSearchRecord>();
            }

            public List<TeacherPlacementSearchRecord> Records { get; set; }
        }

        public class GeographicPoint
        {
            public const int CoordinateSystem = 4326;

            private readonly double _latitude;
            private readonly double _longitude;

            public GeographicPoint(double latitude, double longitude)
            {
                _latitude = latitude;
                _longitude = longitude;
            }

            [JsonProperty(PropertyName = "Lat")]
            public double Latitude
            {
                get { return _latitude; }
            }

            [JsonProperty(PropertyName = "Lon")]
            public double Longitude
            {
                get { return _longitude; }
            }

            public string ToWkt()
            {
                return string.Format("POINT({0} {1})", Longitude, Latitude);
            }
        }


        public class SearchCriteria
        {
            public SearchCriteria()
            {
                Keywords = "Simon";
            }

            public string Keywords { get; set; }
        }
    }
}