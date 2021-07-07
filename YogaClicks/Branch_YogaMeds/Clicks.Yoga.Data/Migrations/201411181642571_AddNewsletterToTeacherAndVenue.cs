namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsletterToTeacherAndVenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venue", "NewsletterOptOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Teacher", "NewsletterOptOut", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teacher", "NewsletterOptOut");
            DropColumn("dbo.Venue", "NewsletterOptOut");
        }
    }
}
