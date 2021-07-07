namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileEmailAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "EmailAddress", c => c.String(maxLength: 254));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profile", "EmailAddress");
        }
    }
}
