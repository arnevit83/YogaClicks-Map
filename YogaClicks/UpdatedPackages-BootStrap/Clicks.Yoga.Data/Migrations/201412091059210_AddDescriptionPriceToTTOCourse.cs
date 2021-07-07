namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionPriceToTTOCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherTrainingCourse", "Description", c => c.String());
            AddColumn("dbo.TeacherTrainingCourse", "Price", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherTrainingCourse", "Price");
            DropColumn("dbo.TeacherTrainingCourse", "Description");
        }
    }
}
