using System.Configuration;
using Clicks.Yoga.Domain.Entities;
using Nest;
using System;
using System.Linq;

namespace Clicks.Yoga.Data.IndexElasticSearch
{
    class Program
    {
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
        static void Main(string[] args)
        {
            var dataContext = new YogaDataContext();
            var entityContext = new YogaEntityContext(dataContext);
            //var searchEngine = new ElasticSearchEngine(entityContext);

            ElasticClient elasticClient = GetElasticClient;

            elasticClient.CreateIndex(ConfigurationManager.AppSettings["ElasticSearchIndex"], c => c
                .AddMapping<SearchRecord>(m => m.TypeName("searchrecords").Properties(p => p
                    .GeoPoint(g => g
                        .Name(n => n.Location).IndexLatLon())
                    .MultiField(mf => mf
                        .Name(n => n.Name)
                        .Fields(f => f
                            .String(s => s
                                .Name("name_notanalyzed")
                                .IncludeInAll(false)
                                .Index(FieldIndexOption.not_analyzed))
                            .String(s => s
                                .Name("name_analyzed")
                                .IncludeInAll(false)
                                .Index(FieldIndexOption.analyzed))))
                    .NestedObject<SearchLink>(no => no
                        .Name(n => n.LinkedEntities.First())
                        .Properties(pr => pr
                            .String(s => s
                                .Name(n => n.TypeName)
                                .Index(FieldIndexOption.not_analyzed))))
                )));


            //foreach (var record in dataContext.SearchRecords.ToList())
            //{
            //    var entity = service.GetEntity<ISearchable>(record.EntityId, record.EntityType.Name);
            //    if (entity == null) continue;

            //    if (record.Image != null)
            //    {
            //        record.ImageGuid = record.Image.Guid;
            //        record.ImagePath = record.Image.Path;
            //        record.ImageExtension = record.Image.Type.Extension;
            //    }

            //    var parent = entity.GetParentEntity() as ISearchable;

            //    if (parent != null)
            //    {
            //        record.Parent = new SearchRecord(parent)
            //        {
            //            EntityType = context.EntityTypes.Get(parent),
            //            CreationTime = DateTime.Now,
            //            ModificationTime = DateTime.Now
            //        };
            //    }

            //    entity.PopulateSearchRecord(record);

            //    elasticClient.Index(record, ConfigurationManager.AppSettings["ElasticSearchIndex"], "searchrecords", record.EntityType.Name + entity.Id.ToString());
            //    Console.WriteLine("Indexed: " + record.Name);
            //}
        }
    }
}

