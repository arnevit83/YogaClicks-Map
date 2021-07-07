namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityAttendenceNullable : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.ActivityAttendee", "Confirmed", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ActivityAttendee", "Confirmed", c => c.Boolean(nullable: false));
        }
    }
}
