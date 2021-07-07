namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsController : DbMigration
    {
        public override void Up()
        {
            Sql("update EntityType set Controller = 'Reviews' where Name = 'Review'");
        }
        
        public override void Down()
        {
            Sql("update EntityType set Controller = null where Name = 'Review'");
        }
    }
}
