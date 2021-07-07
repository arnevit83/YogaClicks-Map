namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityClassType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherService", "IsClass", c => c.Boolean(nullable: true));

            Sql("update TeacherService set IsClass = 0");
            Sql("update TeacherService set IsClass = 1 where Name like '%class%'");

            AlterColumn("dbo.TeacherService", "IsClass", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherService", "IsClass");
        }
    }
}
