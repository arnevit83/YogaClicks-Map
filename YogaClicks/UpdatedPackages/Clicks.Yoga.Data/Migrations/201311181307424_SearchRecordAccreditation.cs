namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchRecordAccreditation : DbMigration
    {
        public override void Up()
        {
//            CreateTable(
//                "dbo.SearchRecordAccreditationBodyLink",
//                c => new
//                    {
//                        RecordId = c.Int(nullable: false),
//                        AccreditationBodyId = c.Int(nullable: false),
//                    })
//                .PrimaryKey(t => new { t.RecordId, t.AccreditationBodyId })
//                .ForeignKey("dbo.SearchRecord", t => t.RecordId, cascadeDelete: true)
//                .ForeignKey("dbo.AccreditationBody", t => t.AccreditationBodyId, cascadeDelete: true)
//                .Index(t => t.RecordId)
//                .Index(t => t.AccreditationBodyId);
            
//            Sql(
//                @"insert SearchRecordAccreditationBodyLink (RecordId, AccreditationBodyId)
//                select r.Id, a.AccreditationBodyId
//                from SearchRecord r
//                inner join TeacherTrainingCourseAccreditations a on r.EntityId = a.CourseId
//                inner join EntityType t on r.EntityTypeId = t.Id
//                where t.Name = 'TeacherTrainingCourse'");

//            Sql(
//                @"insert SearchRecordAccreditationBodyLink (RecordId, AccreditationBodyId)
//                select r.Id, a.AccreditationBodyId
//                from SearchRecord r
//                inner join TeacherAccreditation a on r.EntityId = a.TeacherId
//                inner join EntityType t on r.EntityTypeId = t.Id
//                where t.Name = 'Teacher'");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SearchRecordAccreditationBodyLink", new[] { "AccreditationBodyId" });
            DropIndex("dbo.SearchRecordAccreditationBodyLink", new[] { "RecordId" });
            DropForeignKey("dbo.SearchRecordAccreditationBodyLink", "AccreditationBodyId", "dbo.AccreditationBody");
            DropForeignKey("dbo.SearchRecordAccreditationBodyLink", "RecordId", "dbo.SearchRecord");
            DropTable("dbo.SearchRecordAccreditationBodyLink");
        }
    }
}
