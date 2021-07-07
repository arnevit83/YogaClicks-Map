namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvitationNullableLoginProvider : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invitation", "LoginProviderId", "dbo.FederatedLoginProvider");
            DropIndex("dbo.Invitation", new[] { "LoginProviderId" });
            AlterColumn("dbo.Invitation", "LoginProviderId", c => c.Int());
            AddForeignKey("dbo.Invitation", "LoginProviderId", "dbo.FederatedLoginProvider", "Id");
            CreateIndex("dbo.Invitation", "LoginProviderId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Invitation", new[] { "LoginProviderId" });
            DropForeignKey("dbo.Invitation", "LoginProviderId", "dbo.FederatedLoginProvider");
            AlterColumn("dbo.Invitation", "LoginProviderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invitation", "LoginProviderId");
            AddForeignKey("dbo.Invitation", "LoginProviderId", "dbo.FederatedLoginProvider", "Id", cascadeDelete: true);
        }
    }
}
