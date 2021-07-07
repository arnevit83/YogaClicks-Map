namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ChangeStudyDateTimeToString : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Study", "DateOfStudy", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Study", "DateOfStudy", c => c.DateTime(nullable: false));
        }
    }
}
