namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastDigestEmailSentDate : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.NotificationPreferences", "DateLastEmailDigestSent", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationPreferences", "DateLastEmailDigestSent");
        }
    }
}
