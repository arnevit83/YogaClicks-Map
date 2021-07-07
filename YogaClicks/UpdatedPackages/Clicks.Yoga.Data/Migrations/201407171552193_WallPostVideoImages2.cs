namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WallPostVideoImages2 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.GalleryEntry", "NewsfeedPending", c => c.Boolean(nullable: false));
            //AddColumn("dbo.GalleryEntry", "NewsStoryId", c => c.Int());
            //AddColumn("dbo.GalleryEntry", "WallPostId", c => c.Int());
            //AddForeignKey("dbo.GalleryEntry", "NewsStoryId", "dbo.NewsStory", "Id");
            //AddForeignKey("dbo.GalleryEntry", "WallPostId", "dbo.WallPost", "Id");
            //CreateIndex("dbo.GalleryEntry", "NewsStoryId");
            //CreateIndex("dbo.GalleryEntry", "WallPostId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GalleryEntry", new[] { "WallPostId" });
            DropIndex("dbo.GalleryEntry", new[] { "NewsStoryId" });
            DropForeignKey("dbo.GalleryEntry", "WallPostId", "dbo.WallPost");
            DropForeignKey("dbo.GalleryEntry", "NewsStoryId", "dbo.NewsStory");
            DropColumn("dbo.GalleryEntry", "WallPostId");
            DropColumn("dbo.GalleryEntry", "NewsStoryId");
            DropColumn("dbo.GalleryEntry", "NewsfeedPending");
        }
    }
}
