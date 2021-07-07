namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddYogaMedsEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Condition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImageCourtesyOf = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                        ProfileBanner_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Image", t => t.ProfileBanner_Id)
                .Index(t => t.ProfileBanner_Id);
            
            CreateTable(
                "dbo.Study",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abstract = c.String(),
                        DateOfStudy = c.DateTime(nullable: false),
                        Journal = c.String(),
                        Volume = c.String(),
                        Institution = c.String(),
                        Source = c.String(),
                        NumberOfCitations = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TherapyType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TherapyTypeStudy",
                c => new
                    {
                        TherapyType_Id = c.Int(nullable: false),
                        Study_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TherapyType_Id, t.Study_Id })
                .ForeignKey("dbo.TherapyType", t => t.TherapyType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Study", t => t.Study_Id, cascadeDelete: true)
                .Index(t => t.TherapyType_Id)
                .Index(t => t.Study_Id);
            
            CreateTable(
                "dbo.AuthorStudy",
                c => new
                    {
                        Author_Id = c.Int(nullable: false),
                        Study_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Author_Id, t.Study_Id })
                .ForeignKey("dbo.Author", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Study", t => t.Study_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Study_Id);
            
            CreateTable(
                "dbo.StudyCondition",
                c => new
                    {
                        Study_Id = c.Int(nullable: false),
                        Condition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Study_Id, t.Condition_Id })
                .ForeignKey("dbo.Study", t => t.Study_Id, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .Index(t => t.Study_Id)
                .Index(t => t.Condition_Id);
            
            CreateTable(
                "dbo.ConditionTeacher",
                c => new
                    {
                        Condition_Id = c.Int(nullable: false),
                        Teacher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Condition_Id, t.Teacher_Id })
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.Teacher_Id, cascadeDelete: true)
                .Index(t => t.Condition_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.ConditionVenue",
                c => new
                    {
                        Condition_Id = c.Int(nullable: false),
                        Venue_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Condition_Id, t.Venue_Id })
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .ForeignKey("dbo.Venue", t => t.Venue_Id, cascadeDelete: true)
                .Index(t => t.Condition_Id)
                .Index(t => t.Venue_Id);
            
            CreateTable(
                "dbo.TeacherTrainingOrganisationCondition",
                c => new
                    {
                        TeacherTrainingOrganisation_Id = c.Int(nullable: false),
                        Condition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeacherTrainingOrganisation_Id, t.Condition_Id })
                .ForeignKey("dbo.TeacherTrainingOrganisation", t => t.TeacherTrainingOrganisation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .Index(t => t.TeacherTrainingOrganisation_Id)
                .Index(t => t.Condition_Id);
            
            CreateTable(
                "dbo.ConditionActivity",
                c => new
                    {
                        Condition_Id = c.Int(nullable: false),
                        Activity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Condition_Id, t.Activity_Id })
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activity", t => t.Activity_Id, cascadeDelete: true)
                .Index(t => t.Condition_Id)
                .Index(t => t.Activity_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ConditionActivity", new[] { "Activity_Id" });
            DropIndex("dbo.ConditionActivity", new[] { "Condition_Id" });
            DropIndex("dbo.TeacherTrainingOrganisationCondition", new[] { "Condition_Id" });
            DropIndex("dbo.TeacherTrainingOrganisationCondition", new[] { "TeacherTrainingOrganisation_Id" });
            DropIndex("dbo.ConditionVenue", new[] { "Venue_Id" });
            DropIndex("dbo.ConditionVenue", new[] { "Condition_Id" });
            DropIndex("dbo.ConditionTeacher", new[] { "Teacher_Id" });
            DropIndex("dbo.ConditionTeacher", new[] { "Condition_Id" });
            DropIndex("dbo.StudyCondition", new[] { "Condition_Id" });
            DropIndex("dbo.StudyCondition", new[] { "Study_Id" });
            DropIndex("dbo.AuthorStudy", new[] { "Study_Id" });
            DropIndex("dbo.AuthorStudy", new[] { "Author_Id" });
            DropIndex("dbo.TherapyTypeStudy", new[] { "Study_Id" });
            DropIndex("dbo.TherapyTypeStudy", new[] { "TherapyType_Id" });
            DropIndex("dbo.Condition", new[] { "ProfileBanner_Id" });
            DropForeignKey("dbo.ConditionActivity", "Activity_Id", "dbo.Activity");
            DropForeignKey("dbo.ConditionActivity", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.TeacherTrainingOrganisationCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.TeacherTrainingOrganisationCondition", "TeacherTrainingOrganisation_Id", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.ConditionVenue", "Venue_Id", "dbo.Venue");
            DropForeignKey("dbo.ConditionVenue", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.ConditionTeacher", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.ConditionTeacher", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.StudyCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.StudyCondition", "Study_Id", "dbo.Study");
            DropForeignKey("dbo.AuthorStudy", "Study_Id", "dbo.Study");
            DropForeignKey("dbo.AuthorStudy", "Author_Id", "dbo.Author");
            DropForeignKey("dbo.TherapyTypeStudy", "Study_Id", "dbo.Study");
            DropForeignKey("dbo.TherapyTypeStudy", "TherapyType_Id", "dbo.TherapyType");
            DropForeignKey("dbo.Condition", "ProfileBanner_Id", "dbo.Image");
            DropTable("dbo.ConditionActivity");
            DropTable("dbo.TeacherTrainingOrganisationCondition");
            DropTable("dbo.ConditionVenue");
            DropTable("dbo.ConditionTeacher");
            DropTable("dbo.StudyCondition");
            DropTable("dbo.AuthorStudy");
            DropTable("dbo.TherapyTypeStudy");
            DropTable("dbo.Author");
            DropTable("dbo.TherapyType");
            DropTable("dbo.Study");
            DropTable("dbo.Condition");
        }
    }
}
