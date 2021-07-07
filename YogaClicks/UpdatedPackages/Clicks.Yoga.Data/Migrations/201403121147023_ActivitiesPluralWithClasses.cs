namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivitiesPluralWithClasses : DbMigration
    {
        public override void Up()
        {
            //Sql("update dbo.EntityType set DisplayPlural = 'Activities and Classes' where Name = 'Activity'");
        }
        
        public override void Down()
        {
            Sql("update dbo.EntityType set DisplayPlural = 'Activities' where Name = 'Activity'");
        }
    }
}
