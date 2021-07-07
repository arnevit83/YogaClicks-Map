namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeNewsStoryFanable : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.NewsStory", "Name", c => c.String());
            //AddColumn("dbo.NewsStory", "Active", c => c.Boolean(nullable: false));
            //AddColumn("dbo.NewsStory", "Image_Id", c => c.Int());
            //AddColumn("dbo.NewsStory", "Location_Id", c => c.Int());
            //AddForeignKey("dbo.NewsStory", "Image_Id", "dbo.Image", "Id");
            //AddForeignKey("dbo.NewsStory", "Location_Id", "dbo.Location", "Id");
            //CreateIndex("dbo.NewsStory", "Image_Id");
            //CreateIndex("dbo.NewsStory", "Location_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.NewsStory", new[] { "Location_Id" });
            DropIndex("dbo.NewsStory", new[] { "Image_Id" });
            DropForeignKey("dbo.NewsStory", "Location_Id", "dbo.Location");
            DropForeignKey("dbo.NewsStory", "Image_Id", "dbo.Image");
            DropColumn("dbo.NewsStory", "Location_Id");
            DropColumn("dbo.NewsStory", "Image_Id");
            DropColumn("dbo.NewsStory", "Active");
            DropColumn("dbo.NewsStory", "Name");
        }
    }
}
