namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivitiesController : DbMigration
    {
        public override void Up()
        {
            //Sql("update EntityType set Controller = 'Activities' where Name = 'Activity'");
        }
        
        public override void Down()
        {
            Sql("update EntityType set Controller = null where Name = 'Activity'");
        }
    }
}
