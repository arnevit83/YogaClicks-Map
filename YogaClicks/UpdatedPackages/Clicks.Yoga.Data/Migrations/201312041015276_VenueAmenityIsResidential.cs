namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VenueAmenityIsResidential : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.VenueAmenity", "IsResidential", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VenueAmenity", "IsResidential");
        }
    }
}
