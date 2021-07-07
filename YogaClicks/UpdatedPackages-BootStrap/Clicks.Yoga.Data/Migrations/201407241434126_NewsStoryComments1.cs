namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewsStoryComments1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsStoryComment",
                c => new
                    {
                        StoryId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryId, t.CommentId })
                .ForeignKey("dbo.NewsStory", t => t.StoryId, cascadeDelete: true)
                .ForeignKey("dbo.Comment", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.StoryId)
                .Index(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.NewsStoryComment", new[] { "CommentId" });
            DropIndex("dbo.NewsStoryComment", new[] { "StoryId" });
            DropForeignKey("dbo.NewsStoryComment", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.NewsStoryComment", "StoryId", "dbo.NewsStory");
            DropTable("dbo.NewsStoryComment");
        }
    }
}
