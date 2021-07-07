using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clicks.Yoga.Data;

namespace Cicks.Yoga.DbMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionName = ConfigurationManager.AppSettings["DbConnectionString"];
            var configuration = new DbMigrationsConfiguration<YogaDataContext>();

            //configuration.TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo(connectionName);

            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
