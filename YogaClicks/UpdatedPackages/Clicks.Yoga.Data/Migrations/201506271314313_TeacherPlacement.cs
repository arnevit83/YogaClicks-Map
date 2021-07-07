namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherPlacement : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.SearchRecord", "LocationArray", c => c.String());
            //AddColumn("dbo.TeacherPlacement", "SearchRecord_Id", c => c.Int());
            //CreateIndex("dbo.TeacherPlacement", "SearchRecord_Id");
            //AddForeignKey("dbo.TeacherPlacement", "SearchRecord_Id", "dbo.SearchRecord", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherPlacement", "SearchRecord_Id", "dbo.SearchRecord");
            DropIndex("dbo.TeacherPlacement", new[] { "SearchRecord_Id" });
            DropColumn("dbo.TeacherPlacement", "SearchRecord_Id");
            DropColumn("dbo.SearchRecord", "LocationArray");
        }
    }
}
