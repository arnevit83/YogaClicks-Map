namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherTrainingCourseDurationLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeacherTrainingCourse", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeacherTrainingCourse", "Duration", c => c.String(maxLength: 100));
        }
    }
}
