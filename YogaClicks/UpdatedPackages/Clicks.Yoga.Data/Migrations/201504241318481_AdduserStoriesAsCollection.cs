namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AdduserStoriesAsCollection : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.UserStory",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Content = c.String(),
            //            OwnerHidden = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            Owner_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.Owner_Id)
            //    .Index(t => t.Owner_Id);

            //CreateTable(
            //    "dbo.UserStoryCondition",
            //    c => new
            //        {
            //            UserStory_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserStory_Id, t.Condition_Id })
            //    .ForeignKey("dbo.UserStory", t => t.UserStory_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.UserStory_Id)
            //    .Index(t => t.Condition_Id);

        }

        public override void Down()
        {
            DropIndex("dbo.UserStoryCondition", new[] { "Condition_Id" });
            DropIndex("dbo.UserStoryCondition", new[] { "UserStory_Id" });
            DropIndex("dbo.UserStory", new[] { "Owner_Id" });
            DropForeignKey("dbo.UserStoryCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.UserStoryCondition", "UserStory_Id", "dbo.UserStory");
            DropForeignKey("dbo.UserStory", "Owner_Id", "dbo.User");
            DropTable("dbo.UserStoryCondition");
            DropTable("dbo.UserStory");
        }
    }
}
