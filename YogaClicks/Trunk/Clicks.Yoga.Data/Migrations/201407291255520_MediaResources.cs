namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaResources : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaResource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uri = c.String(),
                        Identifier = c.String(nullable: false, maxLength: 1000),
                        Title = c.String(),
                        Content = c.String(),
                        ExpiryTime = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MediaResourceType", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.Image", t => t.ImageId)
                .Index(t => t.TypeId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.MediaResourceType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(nullable: false, maxLength: 255),
                        UriPattern = c.String(nullable: false, maxLength: 1000),
                        Priority = c.Int(nullable: false),
                        ScannerTypeName = c.String(nullable: false, maxLength: 1024),
                        ExpirySeconds = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WallPostResource",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.ResourceId })
                .ForeignKey("dbo.WallPost", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.MediaResource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.ResourceId);
            
            CreateTable(
                "dbo.NewsStoryResource",
                c => new
                    {
                        StoryId = c.Int(nullable: false),
                        ResourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryId, t.ResourceId })
                .ForeignKey("dbo.NewsStory", t => t.StoryId, cascadeDelete: true)
                .ForeignKey("dbo.MediaResource", t => t.ResourceId, cascadeDelete: true)
                .Index(t => t.StoryId)
                .Index(t => t.ResourceId);

            Sql(
                @"insert into MediaResourceType (Tag, UriPattern, Priority, ScannerTypeName, ExpirySeconds, CreationTime, ModificationTime)
                values ('Website', '^https?://\.*', 1, 'Clicks.Yoga.Media.Scanners.TextWebMediaScanner, Clicks.Yoga', 86400, getdate(), getdate())");
            Sql(
                @"insert into MediaResourceType (Tag, UriPattern, Priority, ScannerTypeName, ExpirySeconds, CreationTime, ModificationTime)
                values ('Image', '^urn:yogaclicks:entity:Image:(.*)', 0, 'Clicks.Yoga.Media.Scanners.ImageMediaScanner, Clicks.Yoga', null, getdate(), getdate())");
            Sql(
                @"insert into MediaResourceType (Tag, UriPattern, Priority, ScannerTypeName, ExpirySeconds, CreationTime, ModificationTime)
                values ('Video', '^urn:yogaclicks:entity:Video:(.*)', 0, 'Clicks.Yoga.Media.Scanners.VideoMediaScanner, Clicks.Yoga', null, getdate(), getdate())");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.NewsStoryResource", new[] { "ResourceId" });
            DropIndex("dbo.NewsStoryResource", new[] { "StoryId" });
            DropIndex("dbo.WallPostResource", new[] { "ResourceId" });
            DropIndex("dbo.WallPostResource", new[] { "PostId" });
            DropIndex("dbo.MediaResource", new[] { "ImageId" });
            DropIndex("dbo.MediaResource", new[] { "TypeId" });
            DropForeignKey("dbo.NewsStoryResource", "ResourceId", "dbo.MediaResource");
            DropForeignKey("dbo.NewsStoryResource", "StoryId", "dbo.NewsStory");
            DropForeignKey("dbo.WallPostResource", "ResourceId", "dbo.MediaResource");
            DropForeignKey("dbo.WallPostResource", "PostId", "dbo.WallPost");
            DropForeignKey("dbo.MediaResource", "ImageId", "dbo.Image");
            DropForeignKey("dbo.MediaResource", "TypeId", "dbo.MediaResourceType");
            DropTable("dbo.NewsStoryResource");
            DropTable("dbo.WallPostResource");
            DropTable("dbo.MediaResourceType");
            DropTable("dbo.MediaResource");
        }
    }
}
