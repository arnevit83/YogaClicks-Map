namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddFollowerEntityAndConditonLink : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Follow",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FollowerId = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            FollowerType_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityType", t => t.FollowerType_Id)
            //    .Index(t => t.FollowerType_Id);

            //CreateTable(
            //    "dbo.FollowCondition",
            //    c => new
            //        {
            //            Follow_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Follow_Id, t.Condition_Id })
            //    .ForeignKey("dbo.Follow", t => t.Follow_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.Follow_Id)
            //    .Index(t => t.Condition_Id);

        }

        public override void Down()
        {
            DropIndex("dbo.FollowCondition", new[] { "Condition_Id" });
            DropIndex("dbo.FollowCondition", new[] { "Follow_Id" });
            DropIndex("dbo.Follow", new[] { "FollowerType_Id" });
            DropForeignKey("dbo.FollowCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.FollowCondition", "Follow_Id", "dbo.Follow");
            DropForeignKey("dbo.Follow", "FollowerType_Id", "dbo.EntityType");
            DropTable("dbo.FollowCondition");
            DropTable("dbo.Follow");
        }
    }
}
