namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YogaMapAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YmStory", "AddressId", c => c.Int());
            AddForeignKey("dbo.YmStory", "AddressId", "dbo.Address", "Id");
            CreateIndex("dbo.YmStory", "AddressId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.YmStory", new[] { "AddressId" });
            DropForeignKey("dbo.YmStory", "AddressId", "dbo.Address");
            DropColumn("dbo.YmStory", "AddressId");
        }
    }
}
