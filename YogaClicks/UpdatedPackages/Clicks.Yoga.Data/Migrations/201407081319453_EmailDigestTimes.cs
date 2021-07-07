namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailDigestTimes : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.NotificationPreferences", "NextDigestEmailTime", c => c.DateTime());
            //DropColumn("dbo.NotificationPreferences", "DateLastEmailDigestSent");
            //Sql(@"update dbo.NotificationPreferences set NextDigestEmailTime = dateadd(w, 1, getdate())");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotificationPreferences", "DateLastEmailDigestSent", c => c.DateTime());
            DropColumn("dbo.NotificationPreferences", "NextDigestEmailTime");
        }
    }
}
