namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConditionToSearchRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SearchRecordCondition",
                c => new
                    {
                        SearchRecord_Id = c.Int(nullable: false),
                        Condition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SearchRecord_Id, t.Condition_Id })
                .ForeignKey("dbo.SearchRecord", t => t.SearchRecord_Id, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .Index(t => t.SearchRecord_Id)
                .Index(t => t.Condition_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SearchRecordCondition", new[] { "Condition_Id" });
            DropIndex("dbo.SearchRecordCondition", new[] { "SearchRecord_Id" });
            DropForeignKey("dbo.SearchRecordCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.SearchRecordCondition", "SearchRecord_Id", "dbo.SearchRecord");
            DropTable("dbo.SearchRecordCondition");
        }
    }
}
