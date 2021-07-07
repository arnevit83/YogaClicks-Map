namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoUpload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.YmStory", "VideoId", c => c.Int());
            AddForeignKey("dbo.YmStory", "VideoId", "dbo.Video", "Id");
            CreateIndex("dbo.YmStory", "VideoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.YmStory", new[] { "VideoId" });
            DropForeignKey("dbo.YmStory", "VideoId", "dbo.Video");
            DropColumn("dbo.YmStory", "VideoId");
        }
    }
}
