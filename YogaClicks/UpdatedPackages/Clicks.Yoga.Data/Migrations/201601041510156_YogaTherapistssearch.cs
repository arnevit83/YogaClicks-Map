namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YogaTherapistssearch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchRecord", "IsAccreditedTherapist", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchRecord", "IsAccreditedTherapist");
        }
    }
}
