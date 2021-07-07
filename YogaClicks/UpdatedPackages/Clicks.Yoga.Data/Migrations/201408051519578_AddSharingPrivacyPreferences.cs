namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSharingPrivacyPreferences : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.PrivacyPreferences",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SharingPermitted = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //AddColumn("dbo.User", "PrivacyPreferencesId", c => c.Int());
            //AddForeignKey("dbo.User", "PrivacyPreferencesId", "dbo.PrivacyPreferences", "Id");
            //CreateIndex("dbo.User", "PrivacyPreferencesId");
            //DropColumn("dbo.Profile", "PostSharingPermitted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Profile", "PostSharingPermitted", c => c.Boolean(nullable: false));
            DropIndex("dbo.User", new[] { "PrivacyPreferencesId" });
            DropForeignKey("dbo.User", "PrivacyPreferencesId", "dbo.PrivacyPreferences");
            DropColumn("dbo.User", "PrivacyPreferencesId");
            DropTable("dbo.PrivacyPreferences");
        }
    }
}
