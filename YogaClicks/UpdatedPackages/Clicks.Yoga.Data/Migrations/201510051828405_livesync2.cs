namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class livesync2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YmStory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Location = c.String(maxLength: 100),
                        PoweredFrom = c.DateTime(nullable: false),
                        ProfileType = c.String(nullable: false, maxLength: 20),
                        Title = c.String(),
                        Story = c.String(),
                        Video = c.String(maxLength: 100),
                        Lat = c.Single(nullable: false),
                        Lng = c.Single(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                        ImageId = c.Int(),
                        OwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Image", t => t.ImageId)
                .ForeignKey("dbo.User", t => t.OwnerId)
                .Index(t => t.ImageId)
                .Index(t => t.OwnerId);
            CreateTable(
                "dbo.YmStoryConditionLink",
                c => new
                    {
                        YmStoryId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryId, t.ConditionId })
                .ForeignKey("dbo.YmStory", t => t.YmStoryId, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.ConditionId, cascadeDelete: true)
                .Index(t => t.YmStoryId)
                .Index(t => t.ConditionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.YmStoryConditionLink", new[] { "ConditionId" });
            DropIndex("dbo.YmStoryConditionLink", new[] { "YmStoryId" });
            DropIndex("dbo.SchoolTeacher", new[] { "Teacher_Id" });
            DropIndex("dbo.SchoolTeacher", new[] { "School_Id" });
            DropIndex("dbo.TeacherTrainingOrganisationQualificationTeacherTrainingOrganisation", new[] { "TeacherTrainingOrganisation_Id" });
            DropIndex("dbo.TeacherTrainingOrganisationQualificationTeacherTrainingOrganisation", new[] { "TeacherTrainingOrganisationQualification_Id" });
            DropIndex("dbo.YmStory", new[] { "OwnerId" });
            DropIndex("dbo.YmStory", new[] { "ImageId" });
            DropIndex("dbo.ActivityVenue", new[] { "VenueId" });
            DropIndex("dbo.ActivityVenue", new[] { "ActivityId" });
            DropIndex("dbo.TeacherTrainingOrganisationFaculty", new[] { "TeacherId" });
            DropIndex("dbo.TeacherTrainingOrganisationFaculty", new[] { "TTOId" });
            DropIndex("dbo.TeacherTrainingOrganisationAccreditation", new[] { "AccreditationBodyId" });
            DropIndex("dbo.TeacherTrainingOrganisationAccreditation", new[] { "TeacherTrainingOrganisationId" });
            DropIndex("dbo.Condition", new[] { "Owner_Id" });
            DropIndex("dbo.Condition", new[] { "Image_Id" });
            DropForeignKey("dbo.YmStoryConditionLink", "ConditionId", "dbo.Condition");
            DropForeignKey("dbo.YmStoryConditionLink", "YmStoryId", "dbo.YmStory");
            DropForeignKey("dbo.SchoolTeacher", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.SchoolTeacher", "School_Id", "dbo.School");
            DropForeignKey("dbo.TeacherTrainingOrganisationQualificationTeacherTrainingOrganisation", "TeacherTrainingOrganisation_Id", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.TeacherTrainingOrganisationQualificationTeacherTrainingOrganisation", "TeacherTrainingOrganisationQualification_Id", "dbo.TeacherTrainingOrganisationQualification");
            DropForeignKey("dbo.YmStory", "OwnerId", "dbo.User");
            DropForeignKey("dbo.YmStory", "ImageId", "dbo.Image");
            DropForeignKey("dbo.ActivityVenue", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.ActivityVenue", "ActivityId", "dbo.Activity");
            DropForeignKey("dbo.TeacherTrainingOrganisationFaculty", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherTrainingOrganisationFaculty", "TTOId", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.TeacherTrainingOrganisationAccreditation", "AccreditationBodyId", "dbo.AccreditationBody");
            DropForeignKey("dbo.TeacherTrainingOrganisationAccreditation", "TeacherTrainingOrganisationId", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.Condition", "Owner_Id", "dbo.User");
            DropForeignKey("dbo.Condition", "Image_Id", "dbo.Image");
            DropColumn("dbo.Pose", "MainVideo");
            DropColumn("dbo.TeacherTrainingOrganisation", "TherapyTeacherTraining");
            DropColumn("dbo.Condition", "Owner_Id");
            DropColumn("dbo.Condition", "Image_Id");
            DropColumn("dbo.Teacher", "YearsTeaching");
            DropColumn("dbo.Teacher", "HoursTraining");
            DropColumn("dbo.Teacher", "IsAccredited");
            DropColumn("dbo.Style", "Intro");
            DropColumn("dbo.Style", "Video");
            DropColumn("dbo.AccreditationBody", "Programmes");
            DropTable("dbo.YmStoryConditionLink");
            DropTable("dbo.SchoolTeacher");
            DropTable("dbo.TeacherTrainingOrganisationQualificationTeacherTrainingOrganisation");
            DropTable("dbo.YmStory");
            DropTable("dbo.ActivityVenue");
            DropTable("dbo.School");
            DropTable("dbo.TeacherTrainingOrganisationFaculty");
            DropTable("dbo.TeacherTrainingOrganisationQualification");
            DropTable("dbo.TeacherTrainingOrganisationAccreditation");
            RenameColumn(table: "dbo.Activity", name: "Venue_Id", newName: "VenueId");
        }
    }
}
