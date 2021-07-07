namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StylesposesRevamp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPoseImageLink", "PoseId", "dbo.Pose");
            DropForeignKey("dbo.UserPoseImageLink", "ImageId", "dbo.Image");
            DropIndex("dbo.UserPoseImageLink", new[] { "PoseId" });
            DropIndex("dbo.UserPoseImageLink", new[] { "ImageId" });
            CreateTable(
                "dbo.PoseUserImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                        PoseId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pose", t => t.PoseId, cascadeDelete: true)
                .ForeignKey("dbo.Image", t => t.ImageId)
                .Index(t => t.PoseId)
                .Index(t => t.ImageId);
            
            DropTable("dbo.UserPoseImageLink");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserPoseImageLink",
                c => new
                    {
                        PoseId = c.Int(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PoseId, t.ImageId });
            
            DropIndex("dbo.PoseUserImages", new[] { "ImageId" });
            DropIndex("dbo.PoseUserImages", new[] { "PoseId" });
            DropForeignKey("dbo.PoseUserImages", "ImageId", "dbo.Image");
            DropForeignKey("dbo.PoseUserImages", "PoseId", "dbo.Pose");
            DropTable("dbo.PoseUserImages");
            CreateIndex("dbo.UserPoseImageLink", "ImageId");
            CreateIndex("dbo.UserPoseImageLink", "PoseId");
            AddForeignKey("dbo.UserPoseImageLink", "ImageId", "dbo.Image", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserPoseImageLink", "PoseId", "dbo.Pose", "Id", cascadeDelete: true);
        }
    }
}
