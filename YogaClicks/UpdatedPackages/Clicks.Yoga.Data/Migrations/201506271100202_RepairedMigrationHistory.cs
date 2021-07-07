namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairedMigrationHistory : DbMigration
    {
        public override void Up()
        {            
            // Was manually added to the DB so need to comment this out so it adds the entry to the migrationhistory table
            //CreateTable(
            //    "dbo.Condition",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Description = c.String(),
            //            MetaTitle = c.String(),
            //            MetaDescription = c.String(),
            //            ImageCourtesyOf = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            DirectoryImage_Id = c.Int(),
            //            ProfileBanner_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Image", t => t.DirectoryImage_Id)
            //    .ForeignKey("dbo.Image", t => t.ProfileBanner_Id)
            //    .Index(t => t.DirectoryImage_Id)
            //    .Index(t => t.ProfileBanner_Id);

            //CreateTable(
            //    "dbo.Study",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Abstract = c.String(),
            //            DateOfStudy = c.String(),
            //            Journal = c.String(),
            //            Volume = c.String(),
            //            Institution = c.String(),
            //            Source = c.String(),
            //            NumberOfCitations = c.Int(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.TherapyType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.WhatTheScienceSays",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Description = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Author",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.UserStory",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Title = c.String(),
            //            Content = c.String(),
            //            OwnerHidden = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            Owner_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.Owner_Id)
            //    .Index(t => t.Owner_Id);

            //CreateTable(
            //    "dbo.Follow",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FollowerId = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            FollowerType_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityType", t => t.FollowerType_Id)
            //    .Index(t => t.FollowerType_Id);

            //CreateTable(
            //    "dbo.TherapyTypeStudy",
            //    c => new
            //        {
            //            TherapyType_Id = c.Int(nullable: false),
            //            Study_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.TherapyType_Id, t.Study_Id })
            //    .ForeignKey("dbo.TherapyType", t => t.TherapyType_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Study", t => t.Study_Id, cascadeDelete: true)
            //    .Index(t => t.TherapyType_Id)
            //    .Index(t => t.Study_Id);

            //CreateTable(
            //    "dbo.WhatTheScienceSaysTherapyType",
            //    c => new
            //        {
            //            WhatTheScienceSays_Id = c.Int(nullable: false),
            //            TherapyType_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.WhatTheScienceSays_Id, t.TherapyType_Id })
            //    .ForeignKey("dbo.WhatTheScienceSays", t => t.WhatTheScienceSays_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.TherapyType", t => t.TherapyType_Id, cascadeDelete: true)
            //    .Index(t => t.WhatTheScienceSays_Id)
            //    .Index(t => t.TherapyType_Id);

            //CreateTable(
            //    "dbo.WhatTheScienceSaysCondition",
            //    c => new
            //        {
            //            WhatTheScienceSays_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.WhatTheScienceSays_Id, t.Condition_Id })
            //    .ForeignKey("dbo.WhatTheScienceSays", t => t.WhatTheScienceSays_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.WhatTheScienceSays_Id)
            //    .Index(t => t.Condition_Id);

            //CreateTable(
            //    "dbo.AuthorStudy",
            //    c => new
            //        {
            //            Author_Id = c.Int(nullable: false),
            //            Study_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Author_Id, t.Study_Id })
            //    .ForeignKey("dbo.Author", t => t.Author_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Study", t => t.Study_Id, cascadeDelete: true)
            //    .Index(t => t.Author_Id)
            //    .Index(t => t.Study_Id);

            //CreateTable(
            //    "dbo.StudyCondition",
            //    c => new
            //        {
            //            Study_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Study_Id, t.Condition_Id })
            //    .ForeignKey("dbo.Study", t => t.Study_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.Study_Id)
            //    .Index(t => t.Condition_Id);

            //CreateTable(
            //    "dbo.ConditionTeacher",
            //    c => new
            //        {
            //            Condition_Id = c.Int(nullable: false),
            //            Teacher_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Condition_Id, t.Teacher_Id })
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.Teacher_Id, cascadeDelete: true)
            //    .Index(t => t.Condition_Id)
            //    .Index(t => t.Teacher_Id);

            //CreateTable(
            //    "dbo.GroupCondition",
            //    c => new
            //        {
            //            Group_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Group_Id, t.Condition_Id })
            //    .ForeignKey("dbo.Group", t => t.Group_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.Group_Id)
            //    .Index(t => t.Condition_Id);

            //CreateTable(
            //    "dbo.ConditionVenue",
            //    c => new
            //        {
            //            Condition_Id = c.Int(nullable: false),
            //            Venue_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Condition_Id, t.Venue_Id })
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.Venue_Id, cascadeDelete: true)
            //    .Index(t => t.Condition_Id)
            //    .Index(t => t.Venue_Id);

            //CreateTable(
            //    "dbo.TeacherTrainingOrganisationCondition",
            //    c => new
            //        {
            //            TeacherTrainingOrganisation_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.TeacherTrainingOrganisation_Id, t.Condition_Id })
            //    .ForeignKey("dbo.TeacherTrainingOrganisation", t => t.TeacherTrainingOrganisation_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.TeacherTrainingOrganisation_Id)
            //    .Index(t => t.Condition_Id);

            //CreateTable(
            //    "dbo.ConditionActivity",
            //    c => new
            //        {
            //            Condition_Id = c.Int(nullable: false),
            //            Activity_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Condition_Id, t.Activity_Id })
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Activity", t => t.Activity_Id, cascadeDelete: true)
            //    .Index(t => t.Condition_Id)
            //    .Index(t => t.Activity_Id);

            //CreateTable(
            //    "dbo.UserStoryCondition",
            //    c => new
            //        {
            //            UserStory_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserStory_Id, t.Condition_Id })
            //    .ForeignKey("dbo.UserStory", t => t.UserStory_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.UserStory_Id)
            //    .Index(t => t.Condition_Id);

            //CreateTable(
            //    "dbo.UserStoryTeacher",
            //    c => new
            //        {
            //            UserStory_Id = c.Int(nullable: false),
            //            Teacher_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserStory_Id, t.Teacher_Id })
            //    .ForeignKey("dbo.UserStory", t => t.UserStory_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.Teacher_Id, cascadeDelete: true)
            //    .Index(t => t.UserStory_Id)
            //    .Index(t => t.Teacher_Id);

            //CreateTable(
            //    "dbo.UserStoryVenue",
            //    c => new
            //        {
            //            UserStory_Id = c.Int(nullable: false),
            //            Venue_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.UserStory_Id, t.Venue_Id })
            //    .ForeignKey("dbo.UserStory", t => t.UserStory_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.Venue_Id, cascadeDelete: true)
            //    .Index(t => t.UserStory_Id)
            //    .Index(t => t.Venue_Id);

            //CreateTable(
            //    "dbo.SearchRecordCondition",
            //    c => new
            //        {
            //            SearchRecord_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.SearchRecord_Id, t.Condition_Id })
            //    .ForeignKey("dbo.SearchRecord", t => t.SearchRecord_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.SearchRecord_Id)
            //    .Index(t => t.Condition_Id);

            //CreateTable(
            //    "dbo.FollowCondition",
            //    c => new
            //        {
            //            Follow_Id = c.Int(nullable: false),
            //            Condition_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Follow_Id, t.Condition_Id })
            //    .ForeignKey("dbo.Follow", t => t.Follow_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
            //    .Index(t => t.Follow_Id)
            //    .Index(t => t.Condition_Id);

            //AddColumn("dbo.TeacherPlacement", "SearchRecord_Id", c => c.Int());
            //AddColumn("dbo.SearchRecord", "LocationArray", c => c.String());
            //AddForeignKey("dbo.TeacherPlacement", "SearchRecord_Id", "dbo.SearchRecord", "Id");
            //CreateIndex("dbo.TeacherPlacement", "SearchRecord_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FollowCondition", new[] { "Condition_Id" });
            DropIndex("dbo.FollowCondition", new[] { "Follow_Id" });
            DropIndex("dbo.SearchRecordCondition", new[] { "Condition_Id" });
            DropIndex("dbo.SearchRecordCondition", new[] { "SearchRecord_Id" });
            DropIndex("dbo.UserStoryVenue", new[] { "Venue_Id" });
            DropIndex("dbo.UserStoryVenue", new[] { "UserStory_Id" });
            DropIndex("dbo.UserStoryTeacher", new[] { "Teacher_Id" });
            DropIndex("dbo.UserStoryTeacher", new[] { "UserStory_Id" });
            DropIndex("dbo.UserStoryCondition", new[] { "Condition_Id" });
            DropIndex("dbo.UserStoryCondition", new[] { "UserStory_Id" });
            DropIndex("dbo.ConditionActivity", new[] { "Activity_Id" });
            DropIndex("dbo.ConditionActivity", new[] { "Condition_Id" });
            DropIndex("dbo.TeacherTrainingOrganisationCondition", new[] { "Condition_Id" });
            DropIndex("dbo.TeacherTrainingOrganisationCondition", new[] { "TeacherTrainingOrganisation_Id" });
            DropIndex("dbo.ConditionVenue", new[] { "Venue_Id" });
            DropIndex("dbo.ConditionVenue", new[] { "Condition_Id" });
            DropIndex("dbo.GroupCondition", new[] { "Condition_Id" });
            DropIndex("dbo.GroupCondition", new[] { "Group_Id" });
            DropIndex("dbo.ConditionTeacher", new[] { "Teacher_Id" });
            DropIndex("dbo.ConditionTeacher", new[] { "Condition_Id" });
            DropIndex("dbo.StudyCondition", new[] { "Condition_Id" });
            DropIndex("dbo.StudyCondition", new[] { "Study_Id" });
            DropIndex("dbo.AuthorStudy", new[] { "Study_Id" });
            DropIndex("dbo.AuthorStudy", new[] { "Author_Id" });
            DropIndex("dbo.WhatTheScienceSaysCondition", new[] { "Condition_Id" });
            DropIndex("dbo.WhatTheScienceSaysCondition", new[] { "WhatTheScienceSays_Id" });
            DropIndex("dbo.WhatTheScienceSaysTherapyType", new[] { "TherapyType_Id" });
            DropIndex("dbo.WhatTheScienceSaysTherapyType", new[] { "WhatTheScienceSays_Id" });
            DropIndex("dbo.TherapyTypeStudy", new[] { "Study_Id" });
            DropIndex("dbo.TherapyTypeStudy", new[] { "TherapyType_Id" });
            DropIndex("dbo.Follow", new[] { "FollowerType_Id" });
            DropIndex("dbo.UserStory", new[] { "Owner_Id" });
            DropIndex("dbo.Condition", new[] { "ProfileBanner_Id" });
            DropIndex("dbo.Condition", new[] { "DirectoryImage_Id" });
            DropIndex("dbo.TeacherPlacement", new[] { "SearchRecord_Id" });
            DropForeignKey("dbo.FollowCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.FollowCondition", "Follow_Id", "dbo.Follow");
            DropForeignKey("dbo.SearchRecordCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.SearchRecordCondition", "SearchRecord_Id", "dbo.SearchRecord");
            DropForeignKey("dbo.UserStoryVenue", "Venue_Id", "dbo.Venue");
            DropForeignKey("dbo.UserStoryVenue", "UserStory_Id", "dbo.UserStory");
            DropForeignKey("dbo.UserStoryTeacher", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.UserStoryTeacher", "UserStory_Id", "dbo.UserStory");
            DropForeignKey("dbo.UserStoryCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.UserStoryCondition", "UserStory_Id", "dbo.UserStory");
            DropForeignKey("dbo.ConditionActivity", "Activity_Id", "dbo.Activity");
            DropForeignKey("dbo.ConditionActivity", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.TeacherTrainingOrganisationCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.TeacherTrainingOrganisationCondition", "TeacherTrainingOrganisation_Id", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.ConditionVenue", "Venue_Id", "dbo.Venue");
            DropForeignKey("dbo.ConditionVenue", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.GroupCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.GroupCondition", "Group_Id", "dbo.Group");
            DropForeignKey("dbo.ConditionTeacher", "Teacher_Id", "dbo.Teacher");
            DropForeignKey("dbo.ConditionTeacher", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.StudyCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.StudyCondition", "Study_Id", "dbo.Study");
            DropForeignKey("dbo.AuthorStudy", "Study_Id", "dbo.Study");
            DropForeignKey("dbo.AuthorStudy", "Author_Id", "dbo.Author");
            DropForeignKey("dbo.WhatTheScienceSaysCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.WhatTheScienceSaysCondition", "WhatTheScienceSays_Id", "dbo.WhatTheScienceSays");
            DropForeignKey("dbo.WhatTheScienceSaysTherapyType", "TherapyType_Id", "dbo.TherapyType");
            DropForeignKey("dbo.WhatTheScienceSaysTherapyType", "WhatTheScienceSays_Id", "dbo.WhatTheScienceSays");
            DropForeignKey("dbo.TherapyTypeStudy", "Study_Id", "dbo.Study");
            DropForeignKey("dbo.TherapyTypeStudy", "TherapyType_Id", "dbo.TherapyType");
            DropForeignKey("dbo.Follow", "FollowerType_Id", "dbo.EntityType");
            DropForeignKey("dbo.UserStory", "Owner_Id", "dbo.User");
            DropForeignKey("dbo.Condition", "ProfileBanner_Id", "dbo.Image");
            DropForeignKey("dbo.Condition", "DirectoryImage_Id", "dbo.Image");
            DropForeignKey("dbo.TeacherPlacement", "SearchRecord_Id", "dbo.SearchRecord");
            DropColumn("dbo.SearchRecord", "LocationArray");
            DropColumn("dbo.TeacherPlacement", "SearchRecord_Id");
            DropTable("dbo.FollowCondition");
            DropTable("dbo.SearchRecordCondition");
            DropTable("dbo.UserStoryVenue");
            DropTable("dbo.UserStoryTeacher");
            DropTable("dbo.UserStoryCondition");
            DropTable("dbo.ConditionActivity");
            DropTable("dbo.TeacherTrainingOrganisationCondition");
            DropTable("dbo.ConditionVenue");
            DropTable("dbo.GroupCondition");
            DropTable("dbo.ConditionTeacher");
            DropTable("dbo.StudyCondition");
            DropTable("dbo.AuthorStudy");
            DropTable("dbo.WhatTheScienceSaysCondition");
            DropTable("dbo.WhatTheScienceSaysTherapyType");
            DropTable("dbo.TherapyTypeStudy");
            DropTable("dbo.Follow");
            DropTable("dbo.UserStory");
            DropTable("dbo.Author");
            DropTable("dbo.WhatTheScienceSays");
            DropTable("dbo.TherapyType");
            DropTable("dbo.Study");
            DropTable("dbo.Condition");
        }
    }
}
