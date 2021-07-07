namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddQualificationAndTeacherLink : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Qualification",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.QualificationTeacher",
            //    c => new
            //        {
            //            Qualification_Id = c.Int(nullable: false),
            //            Teacher_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Qualification_Id, t.Teacher_Id })
            //    .ForeignKey("dbo.Qualification", t => t.Qualification_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.Teacher_Id, cascadeDelete: true)
            //    .Index(t => t.Qualification_Id)
            //    .Index(t => t.Teacher_Id);

        }

        public override void Down()
        {
            DropIndex("dbo.QualificationTeacher", new[] { "Teacher_Id" });
            DropIndex("dbo.QualificationTeacher", new[] { "Qualification_Id" });
            DropForeignKey("dbo.QualificationTeacher", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.QualificationTeacher", "Qualification_Id", "dbo.Qualification");
            DropTable("dbo.QualificationTeacher");
            DropTable("dbo.Qualification");
        }
    }
}
