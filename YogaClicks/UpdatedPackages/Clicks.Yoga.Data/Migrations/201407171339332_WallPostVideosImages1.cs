namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WallPostVideosImages1 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.WallPost", "VideoId", c => c.Int());
            //AddColumn("dbo.Gallery", "IsNewsfeed", c => c.Boolean(nullable: false));
            //AddColumn("dbo.NewsStory", "VideoId", c => c.Int());
            //AddForeignKey("dbo.WallPost", "VideoId", "dbo.Video", "Id");
            //AddForeignKey("dbo.NewsStory", "VideoId", "dbo.Video", "Id");
            //CreateIndex("dbo.WallPost", "VideoId");
            //CreateIndex("dbo.NewsStory", "VideoId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.NewsStory", new[] { "VideoId" });
            DropIndex("dbo.WallPost", new[] { "VideoId" });
            DropForeignKey("dbo.NewsStory", "VideoId", "dbo.Video");
            DropForeignKey("dbo.WallPost", "VideoId", "dbo.Video");
            DropColumn("dbo.NewsStory", "VideoId");
            DropColumn("dbo.Gallery", "IsNewsfeed");
            DropColumn("dbo.WallPost", "VideoId");
        }
    }
}
