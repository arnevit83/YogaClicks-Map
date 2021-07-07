namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityHandleInvites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EntityHandleInvite",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                        EntityHandleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EntityHandle", t => t.EntityHandleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.EntityHandleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.EntityHandleInvite", new[] { "UserId" });
            DropIndex("dbo.EntityHandleInvite", new[] { "EntityHandleId" });
            DropForeignKey("dbo.EntityHandleInvite", "UserId", "dbo.User");
            DropForeignKey("dbo.EntityHandleInvite", "EntityHandleId", "dbo.EntityHandle");
            DropTable("dbo.EntityHandleInvite");
        }
    }
}
