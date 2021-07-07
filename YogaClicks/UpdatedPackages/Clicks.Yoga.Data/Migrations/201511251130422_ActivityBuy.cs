namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityBuy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "ProductDescription", c => c.String());
            AddColumn("dbo.Activity", "ProductButton", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activity", "ProductButton");
            DropColumn("dbo.Activity", "ProductDescription");
        }
    }
}
