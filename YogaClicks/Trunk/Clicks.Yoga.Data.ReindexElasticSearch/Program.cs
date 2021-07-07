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
            var settings = new ConnectionSettings(localhost).SetDefaultIndex(ConfigurationManager.AppSettings["ElasticSearchIndex"]);
            var client = new ElasticClient(settings);

            var elasticIds = new Dictionary<string, bool>();
            var elasticRecords = client.Search<SearchRecord>(q => q.MatchAll().Take(100000)).Documents.ToList();

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(ISearchable).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract)
                .OrderBy(t => t.Name);

            foreach (var type in types)
            {
                var set = dataContext.Set(type);
                var entities = new List<ISearchable>();

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
                Console.WriteLine("Elastic Search Record {0}", record.EntityType.Name);
                if (record.EntityType == null) continue;
                
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