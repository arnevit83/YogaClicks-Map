namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddTeachersVenuesToUserStory : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.UserStoryTeacher",
            //    c => new
            //        {
            //            UserStory_Id = c.Int(nullable: false),
            //            Teacher_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserStory_Id, t.Teacher_Id })
            //    .ForeignKey("dbo.UserStory", t => t.UserStory_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.Teacher_Id, cascadeDelete: true)
            //    .Index(t => t.UserStory_Id)
            //    .Index(t => t.Teacher_Id);

            //CreateTable(
            //    "dbo.UserStoryVenue",
            //    c => new
            //        {
            //            UserStory_Id = c.Int(nullable: false),
            //            Venue_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserStory_Id, t.Venue_Id })
            //    .ForeignKey("dbo.UserStory", t => t.UserStory_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.Venue_Id, cascadeDelete: true)
            //    .Index(t => t.UserStory_Id)
            //    .Index(t => t.Venue_Id);

        }

        public override void Down()
        {
            DropIndex("dbo.UserStoryVenue", new[] { "Venue_Id" });
            DropIndex("dbo.UserStoryVenue", new[] { "UserStory_Id" });
            DropIndex("dbo.UserStoryTeacher", new[] { "Teacher_Id" });
            DropIndex("dbo.UserStoryTeacher", new[] { "UserStory_Id" });
            DropForeignKey("dbo.UserStoryVenue", "Venue_Id", "dbo.Venue");
            DropForeignKey("dbo.UserStoryVenue", "UserStory_Id", "dbo.UserStory");
            DropForeignKey("dbo.UserStoryTeacher", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.UserStoryTeacher", "UserStory_Id", "dbo.UserStory");
            DropTable("dbo.UserStoryVenue");
            DropTable("dbo.UserStoryTeacher");
        }
    }
}
