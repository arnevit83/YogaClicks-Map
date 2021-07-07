namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TToCYogaMeds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConditionTeacherTrainingCourse",
                c => new
                    {
                        OrganisationId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrganisationId, t.ConditionId })
                .ForeignKey("dbo.TeacherTrainingCourse", t => t.OrganisationId, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.ConditionId, cascadeDelete: true)
                .Index(t => t.OrganisationId)
                .Index(t => t.ConditionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ConditionTeacherTrainingCourse", new[] { "ConditionId" });
            DropIndex("dbo.ConditionTeacherTrainingCourse", new[] { "OrganisationId" });
            DropForeignKey("dbo.ConditionTeacherTrainingCourse", "ConditionId", "dbo.Condition");
            DropForeignKey("dbo.ConditionTeacherTrainingCourse", "OrganisationId", "dbo.TeacherTrainingCourse");
            DropTable("dbo.ConditionTeacherTrainingCourse");
        }
    }
}
