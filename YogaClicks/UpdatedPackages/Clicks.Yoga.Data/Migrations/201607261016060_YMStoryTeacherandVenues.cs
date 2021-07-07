namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YMStoryTeacherandVenues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YmStoryTeacherLink",
                c => new
                    {
                        YmStoryId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryId, t.TeacherId })
                .ForeignKey("dbo.YmStory", t => t.YmStoryId, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.YmStoryId)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.YmStoryVenueLink",
                c => new
                    {
                        YmStoryId = c.Int(nullable: false),
                        VenueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryId, t.VenueId })
                .ForeignKey("dbo.YmStory", t => t.YmStoryId, cascadeDelete: true)
                .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.YmStoryId)
                .Index(t => t.VenueId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.YmStoryVenueLink", new[] { "VenueId" });
            DropIndex("dbo.YmStoryVenueLink", new[] { "YmStoryId" });
            DropIndex("dbo.YmStoryTeacherLink", new[] { "TeacherId" });
            DropIndex("dbo.YmStoryTeacherLink", new[] { "YmStoryId" });
            DropForeignKey("dbo.YmStoryVenueLink", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.YmStoryVenueLink", "YmStoryId", "dbo.YmStory");
            DropForeignKey("dbo.YmStoryTeacherLink", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.YmStoryTeacherLink", "YmStoryId", "dbo.YmStory");
            DropTable("dbo.YmStoryVenueLink");
            DropTable("dbo.YmStoryTeacherLink");
        }
    }
}
