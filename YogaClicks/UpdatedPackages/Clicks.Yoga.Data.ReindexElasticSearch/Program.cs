using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Clicks.Yoga.Data.Repositories;
using Clicks.Yoga.Data.Search;
using Clicks.Yoga.Domain.Entities;
using Nest;

namespace Clicks.Yoga.Data.ReindexElasticSearch
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dataContext = new YogaDataContext();
            var entityContext = new YogaEntityContext(dataContext);
            var searchEngine = new ElasticSearchEngine(entityContext);

            entityContext.GetType()
                .GetProperty("EntityTypes")
                .SetValue(entityContext, new EntityTypeRepository(dataContext));

            var localhost = new Uri(ConfigurationManager.AppSettings["ElasticSearchHost"]);
            var settings = new ConnectionSettings(localhost).SetDefaultIndex(ConfigurationManager.AppSettings["ElasticSearchIndex"]).SetMaximumAsyncConnections(60);;
            var client = new ElasticClient(settings);

            var elasticIds = new Dictionary<string, bool>();
            var elasticRecords = client.Search<SearchRecord>(q => q.MatchAll().Take(10000000)).Documents.ToList();

            if (elasticRecords.Count == 0)
            {
                client.CreateIndex(ConfigurationManager.AppSettings["ElasticSearchIndex"], c => c
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
            }

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ISearchable).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .OrderBy(t => t.Name);

            foreach (var type in types)
            {
                var set = dataContext.Set(type);
                var entities = new List<ISearchable>();

                //Getting each row...
                foreach (var entity in set)
                {
                    entities.Add((ISearchable)entity);
                }


                foreach (var entity in entities)
                {
                    Console.WriteLine("Indexing {0} {1}", entity.GetEntityTypeName(), entity.Id);
                    searchEngine.Index(entity);
                    var elasticId = entity.GetEntityTypeName() + entity.Id;
                    Console.WriteLine("Elastic Search ID {0}", elasticId);
                    elasticIds.Add(elasticId, true);
                }
            }

            foreach (var record in elasticRecords)
            {
                if (record.EntityType == null) continue;
                Console.WriteLine("Elastic Search Record {0}", record.EntityType.Name);

                var elasticId = record.EntityType.Name + record.EntityId;

                if (!elasticIds.ContainsKey(elasticId))
                {
                    Console.WriteLine("Deleting {0} {1}", record.EntityType.Name, record.EntityId);
                    client.DeleteById<SearchRecord>(elasticId);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}