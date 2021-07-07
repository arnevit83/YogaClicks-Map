namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HiddenProfessionalProfiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "Professional", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "Professional");
        }
    }
}
