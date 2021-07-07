namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDescriptionfieldforTTO : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.TeacherTrainingOrganisation", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherTrainingOrganisation", "Description");
        }
    }
}
