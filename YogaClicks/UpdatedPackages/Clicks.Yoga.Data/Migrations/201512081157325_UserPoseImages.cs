namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPoseImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPoseImageLink",
                c => new
                    {
                        PoseId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PoseId, t.ImageId })
                .ForeignKey("dbo.Pose", t => t.PoseId, cascadeDelete: true)
                .ForeignKey("dbo.Image", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.PoseId)
                .Index(t => t.ImageId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserPoseImageLink", new[] { "ImageId" });
            DropIndex("dbo.UserPoseImageLink", new[] { "PoseId" });
            DropForeignKey("dbo.UserPoseImageLink", "ImageId", "dbo.Image");
            DropForeignKey("dbo.UserPoseImageLink", "PoseId", "dbo.Pose");
            DropTable("dbo.UserPoseImageLink");
        }
    }
}
