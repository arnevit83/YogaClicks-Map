namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnnecessaryMediaProperties : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.WallPost", "VideoId", "dbo.Video");
            //DropForeignKey("dbo.GalleryEntry", "NewsStoryId", "dbo.NewsStory");
            //DropForeignKey("dbo.GalleryEntry", "WallPostId", "dbo.WallPost");
            //DropForeignKey("dbo.NewsStory", "VideoId", "dbo.Video");
            //DropIndex("dbo.WallPost", new[] { "VideoId" });
            //DropIndex("dbo.GalleryEntry", new[] { "NewsStoryId" });
            //DropIndex("dbo.GalleryEntry", new[] { "WallPostId" });
            //DropIndex("dbo.NewsStory", new[] { "VideoId" });
            //DropColumn("dbo.WallPost", "VideoId");
            //DropColumn("dbo.GalleryEntry", "NewsfeedPending");
            //DropColumn("dbo.GalleryEntry", "NewsStoryId");
            //DropColumn("dbo.GalleryEntry", "WallPostId");
            //DropColumn("dbo.NewsStory", "VideoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsStory", "VideoId", c => c.Int());
            AddColumn("dbo.GalleryEntry", "WallPostId", c => c.Int());
            AddColumn("dbo.GalleryEntry", "NewsStoryId", c => c.Int());
            AddColumn("dbo.GalleryEntry", "NewsfeedPending", c => c.Boolean(nullable: false));
            AddColumn("dbo.WallPost", "VideoId", c => c.Int());
            CreateIndex("dbo.NewsStory", "VideoId");
            CreateIndex("dbo.GalleryEntry", "WallPostId");
            CreateIndex("dbo.GalleryEntry", "NewsStoryId");
            CreateIndex("dbo.WallPost", "VideoId");
            AddForeignKey("dbo.NewsStory", "VideoId", "dbo.Video", "Id");
            AddForeignKey("dbo.GalleryEntry", "WallPostId", "dbo.WallPost", "Id");
            AddForeignKey("dbo.GalleryEntry", "NewsStoryId", "dbo.NewsStory", "Id");
            AddForeignKey("dbo.WallPost", "VideoId", "dbo.Video", "Id");
        }
    }
}
