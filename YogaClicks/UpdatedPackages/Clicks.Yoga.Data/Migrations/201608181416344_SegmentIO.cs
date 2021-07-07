namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SegmentIO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "YogaPro", c => c.Boolean(nullable: false));
            AddColumn("dbo.Profile", "HasStory", c => c.Boolean(nullable: false));
            AddColumn("dbo.Profile", "LastLoggedIn", c => c.DateTime());
            AddColumn("dbo.Profile", "NumberofLoginIn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "NumberofLoginIn");
            DropColumn("dbo.Profile", "LastLoggedIn");
            DropColumn("dbo.Profile", "HasStory");
            DropColumn("dbo.Profile", "YogaPro");
        }
    }
}
