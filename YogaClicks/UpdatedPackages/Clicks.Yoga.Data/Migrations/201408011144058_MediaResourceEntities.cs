namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaResourceEntities : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.MediaResource", "EntityId", c => c.Int());
            //CreateIndex("dbo.MediaResource", "EntityId");
            //AddColumn("dbo.MediaResourceType", "EntityTypeId", c => c.Int());
            //AlterColumn("dbo.MediaResource", "Identifier", c => c.String(maxLength: 1000));
            //CreateIndex("dbo.MediaResource", "Identifier");
            //AddForeignKey("dbo.MediaResourceType", "EntityTypeId", "dbo.EntityType", "Id");
            //CreateIndex("dbo.MediaResourceType", "EntityTypeId");

            //Sql(@"update m set EntityTypeId = t.Id from dbo.MediaResourceType m inner join EntityType t on t.Name = 'GalleryEntry' where m.Tag = 'Image'");
            //Sql(@"update m set EntityTypeId = t.Id from dbo.MediaResourceType m inner join EntityType t on t.Name = 'Video' where m.Tag = 'Video'");
        }
        
        public override void Down()
        {

            DropIndex("dbo.MediaResource", new[] { "EntityId" });
            DropIndex("dbo.MediaResource", new[] { "Identifier" });
            DropIndex("dbo.MediaResourceType", new[] { "EntityTypeId" });
            DropForeignKey("dbo.MediaResourceType", "EntityTypeId", "dbo.EntityType");
            Sql(@"update dbo.MediaResource set Identifier = '' where Identifier is null");
            AlterColumn("dbo.MediaResource", "Identifier", c => c.String(nullable: false, maxLength: 1000));
            DropColumn("dbo.MediaResourceType", "EntityTypeId");
            DropColumn("dbo.MediaResource", "EntityId");
        }
    }
}
