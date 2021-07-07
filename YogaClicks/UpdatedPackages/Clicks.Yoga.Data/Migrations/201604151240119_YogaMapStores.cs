namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YogaMapStores : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YmStory", "ShopUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.YmStory", "ShopUrl");
        }
    }
}
