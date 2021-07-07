namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherTrainingOrganisationEmailAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherTrainingOrganisation", "EmailAddress", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherTrainingOrganisation", "EmailAddress");
        }
    }
}
