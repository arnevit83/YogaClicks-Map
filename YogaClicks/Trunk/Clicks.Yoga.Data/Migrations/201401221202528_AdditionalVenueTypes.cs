namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalVenueTypes : DbMigration
    {
        public override void Up()
        {
            Sql("insert VenueType (Name, IsResidential, CreationTime, ModificationTime) values ('Private home', 0, getdate(), getdate())");
            Sql("insert VenueType (Name, IsResidential, CreationTime, ModificationTime) values ('Yurt', 1, getdate(), getdate())");
            Sql("insert VenueType (Name, IsResidential, CreationTime, ModificationTime) values ('Religious space', 1, getdate(), getdate())");
        }
        
        public override void Down()
        {
        }
    }
}
