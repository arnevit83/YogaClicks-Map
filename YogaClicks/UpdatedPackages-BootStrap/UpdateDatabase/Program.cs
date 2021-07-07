using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            DbMigrator migrator = new DbMigrator(new DbMigrationsConfiguration<Clicks.Yoga.Data.YogaDataContext>());
            migrator.Configuration.AutomaticMigrationsEnabled = true;
            migrator.Update();
        }
    }
}
