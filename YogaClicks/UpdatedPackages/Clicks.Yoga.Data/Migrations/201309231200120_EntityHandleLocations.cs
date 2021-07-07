namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityHandleLocations : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.EntityHandle", "Location", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityHandle", "Location");
        }
    }
}
