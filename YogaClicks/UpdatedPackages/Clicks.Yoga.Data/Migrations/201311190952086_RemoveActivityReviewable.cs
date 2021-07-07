namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveActivityReviewable : DbMigration
    {
        public override void Up()
        {
            //Sql("update EntityType set IsReviewable = 0 where Name = 'Activity'");
        }
        
        public override void Down()
        {
            Sql("update EntityType set IsReviewable = 1 where Name = 'Activity'");
        }
    }
}
