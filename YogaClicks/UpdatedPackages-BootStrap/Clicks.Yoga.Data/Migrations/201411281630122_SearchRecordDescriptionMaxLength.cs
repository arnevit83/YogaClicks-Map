namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchRecordDescriptionMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SearchRecord", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SearchRecord", "Description", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
