namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConditionsToGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupCondition",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Condition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Condition_Id })
                .ForeignKey("dbo.Group", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.Condition_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.GroupCondition", new[] { "Condition_Id" });
            DropIndex("dbo.GroupCondition", new[] { "Group_Id" });
            DropForeignKey("dbo.GroupCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.GroupCondition", "Group_Id", "dbo.Group");
            DropTable("dbo.GroupCondition");
        }
    }
}
