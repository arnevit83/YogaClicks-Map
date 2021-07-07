namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class websitelengthincrease : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Website", "Url", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Website", "Url", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
