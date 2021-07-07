namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEmailAddressChangeRequests : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.PasswordLogin", "UserId", "dbo.User");
            //DropIndex("dbo.PasswordLogin", new[] { "UserId" });
            //CreateTable(
            //    "dbo.UserEmailAddressChangeAction",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ClientAddress = c.String(nullable: false, maxLength: 39),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            RequestId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.UserEmailAddressChangeRequest", t => t.RequestId)
            //    .Index(t => t.RequestId);
            
            //CreateTable(
            //    "dbo.UserEmailAddressChangeRequest",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ClientAddress = c.String(nullable: false, maxLength: 39),
            //            Key = c.String(nullable: false, maxLength: 50),
            //            EmailAddress = c.String(nullable: false, maxLength: 254),
            //            ExpiryTime = c.DateTime(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //AddForeignKey("dbo.PasswordLogin", "UserId", "dbo.User", "Id", cascadeDelete: true);
            //CreateIndex("dbo.PasswordLogin", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserEmailAddressChangeRequest", new[] { "UserId" });
            DropIndex("dbo.UserEmailAddressChangeAction", new[] { "RequestId" });
            DropIndex("dbo.PasswordLogin", new[] { "UserId" });
            DropForeignKey("dbo.UserEmailAddressChangeRequest", "UserId", "dbo.User");
            DropForeignKey("dbo.UserEmailAddressChangeAction", "RequestId", "dbo.UserEmailAddressChangeRequest");
            DropForeignKey("dbo.PasswordLogin", "UserId", "dbo.User");
            DropTable("dbo.UserEmailAddressChangeRequest");
            DropTable("dbo.UserEmailAddressChangeAction");
            CreateIndex("dbo.PasswordLogin", "UserId");
            AddForeignKey("dbo.PasswordLogin", "UserId", "dbo.User", "Id");
        }
    }
}
