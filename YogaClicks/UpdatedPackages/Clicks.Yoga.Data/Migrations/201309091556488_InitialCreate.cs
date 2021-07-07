namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.AbilityLevel",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Index = c.Byte(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.AccreditationBody",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Abbreviation = c.String(maxLength: 10),
            //            Description = c.String(nullable: false),
            //            DateFounded = c.DateTime(),
            //            EmailAddress = c.String(),
            //            Telephone = c.String(),
            //            Purpose = c.String(),
            //            MembershipCount = c.Int(),
            //            Founder = c.String(),
            //            ImageCourtesyOf = c.String(),
            //            Affiliations = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            WebsiteId = c.Int(),
            //            ImageId = c.Int(),
            //            AddressId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Address", t => t.AddressId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.WebsiteId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.AddressId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.Website",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Url = c.String(nullable: false, maxLength: 100),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Image",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Path = c.String(nullable: false),
            //            Guid = c.Guid(nullable: false),
            //            Temporary = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.ImageType", t => t.TypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.ImageType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            MimeType = c.String(nullable: false, maxLength: 50, unicode: false),
            //            Extension = c.String(nullable: false, maxLength: 3, unicode: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.User",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EmailAddress = c.String(nullable: false, maxLength: 254),
            //            Active = c.Boolean(nullable: false),
            //            IsSuperUser = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Profile",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Forename = c.String(nullable: false, maxLength: 50),
            //            Surname = c.String(nullable: false, maxLength: 50),
            //            BirthDate = c.DateTime(),
            //            IsFirstVisit = c.Boolean(nullable: false),
            //            ThirdPartyOptOut = c.Boolean(nullable: false),
            //            NewsletterOptOut = c.Boolean(nullable: false),
            //            Public = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            GenderId = c.Int(),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            LocationId = c.Int(),
            //            WallId = c.Int(nullable: false),
            //            OwnerId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Gender", t => t.GenderId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.Location", t => t.LocationId)
            //    .ForeignKey("dbo.ProfileWall", t => t.WallId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.GenderId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.LocationId)
            //    .Index(t => t.WallId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.Gender",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Tag = c.String(),
            //            Name = c.String(nullable: false, maxLength: 7),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.ProfileAnswer",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ProfileId = c.Int(nullable: false),
            //            QuestionId = c.Int(nullable: false),
            //            Text = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Id, t.ProfileId, t.QuestionId })
            //    .ForeignKey("dbo.ProfileQuestion", t => t.QuestionId, cascadeDelete: true)
            //    .ForeignKey("dbo.Profile", t => t.ProfileId, cascadeDelete: true)
            //    .Index(t => t.QuestionId)
            //    .Index(t => t.ProfileId);
            
            //CreateTable(
            //    "dbo.ProfileQuestion",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Text = c.String(nullable: false, maxLength: 100),
            //            DisplayOrder = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Location",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(maxLength: 100),
            //            Geography = c.Geography(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Friendship",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Pending = c.Boolean(nullable: false),
            //            Confirmed = c.Boolean(nullable: false),
            //            BlockedByProfile = c.Boolean(nullable: false),
            //            BlockedByFriend = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ProfileId = c.Int(nullable: false),
            //            FriendId = c.Int(nullable: false),
            //            InverseId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Profile", t => t.ProfileId, cascadeDelete: true)
            //    .ForeignKey("dbo.Profile", t => t.FriendId)
            //    .ForeignKey("dbo.Friendship", t => t.InverseId)
            //    .Index(t => t.ProfileId)
            //    .Index(t => t.FriendId)
            //    .Index(t => t.InverseId);
            
            //CreateTable(
            //    "dbo.ActivityAttendee",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Confirmed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ActivityId = c.Int(nullable: false),
            //            ProfileId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Activity", t => t.ActivityId, cascadeDelete: true)
            //    .ForeignKey("dbo.Profile", t => t.ProfileId, cascadeDelete: true)
            //    .Index(t => t.ActivityId)
            //    .Index(t => t.ProfileId);
            
            //CreateTable(
            //    "dbo.Activity",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Description = c.String(nullable: false),
            //            StartTime = c.DateTime(nullable: false),
            //            FinishTime = c.DateTime(nullable: false),
            //            BookingRequired = c.Boolean(nullable: false),
            //            Price = c.String(nullable: false),
            //            Public = c.Boolean(nullable: false),
            //            AttendeeInvitationsPermitted = c.Boolean(nullable: false),
            //            AttendeePostingPermitted = c.Boolean(nullable: false),
            //            AttendeeGalleriesPermitted = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //            VenueId = c.Int(),
            //            PromoterHandleId = c.Int(nullable: false),
            //            AbilityLevelId = c.Int(),
            //            RepeatFrequencyId = c.Int(),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            WallId = c.Int(nullable: false),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.ActivityType", t => t.TypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.VenueId)
            //    .ForeignKey("dbo.EntityHandle", t => t.PromoterHandleId, cascadeDelete: true)
            //    .ForeignKey("dbo.AbilityLevel", t => t.AbilityLevelId)
            //    .ForeignKey("dbo.ActivityRepeatFrequency", t => t.RepeatFrequencyId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.ActivityWall", t => t.WallId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.VenueId)
            //    .Index(t => t.PromoterHandleId)
            //    .Index(t => t.AbilityLevelId)
            //    .Index(t => t.RepeatFrequencyId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.WallId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.ActivityType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            IsClass = c.Boolean(nullable: false),
            //            IsEvent = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Venue",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 70),
            //            Description = c.String(),
            //            EmailAddress = c.String(),
            //            Telephone = c.String(),
            //            Stubbed = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            TypeId = c.Int(),
            //            AddressId = c.Int(),
            //            BlogId = c.Int(),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.VenueType", t => t.TypeId)
            //    .ForeignKey("dbo.Address", t => t.AddressId)
            //    .ForeignKey("dbo.Website", t => t.BlogId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.AddressId)
            //    .Index(t => t.BlogId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.VenueType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            IsResidential = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Address",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Line1 = c.String(maxLength: 100),
            //            Line2 = c.String(maxLength: 100),
            //            Line3 = c.String(maxLength: 100),
            //            City = c.String(maxLength: 100),
            //            Area = c.String(maxLength: 100),
            //            Postcode = c.String(maxLength: 20),
            //            Location = c.Geography(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            CountryId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Country", t => t.CountryId)
            //    .Index(t => t.CountryId);
            
            //CreateTable(
            //    "dbo.Country",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Code = c.String(nullable: false, maxLength: 2, fixedLength: true),
            //            EnglishName = c.String(nullable: false, maxLength: 50),
            //            Visible = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Style",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            Founder = c.String(nullable: false, maxLength: 100),
            //            FoundingTime = c.String(nullable: false, maxLength: 100),
            //            Location = c.String(maxLength: 100),
            //            Description = c.String(nullable: false),
            //            ImageCourtesyOf = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ImageId = c.Int(),
            //            DirectoryImageId = c.Int(),
            //            WebsiteId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.DirectoryImageId)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.DirectoryImageId)
            //    .Index(t => t.WebsiteId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.StyleTrait",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            GroupId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.StyleTraitGroup", t => t.GroupId, cascadeDelete: true)
            //    .Index(t => t.GroupId);
            
            //CreateTable(
            //    "dbo.StyleTraitGroup",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            DisplayOrder = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.VenueService",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            DisplayOrder = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.VenueAmenity",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.TeacherPlacement",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Confirmed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            TeacherId = c.Int(nullable: false),
            //            VenueId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .Index(t => t.TeacherId)
            //    .Index(t => t.VenueId);
            
            //CreateTable(
            //    "dbo.Teacher",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Philosophy = c.String(),
            //            Telephone = c.String(),
            //            EmailAddress = c.String(maxLength: 254),
            //            Stubbed = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            AddressId = c.Int(),
            //            LocationId = c.Int(),
            //            BlogId = c.Int(),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Address", t => t.AddressId)
            //    .ForeignKey("dbo.Location", t => t.LocationId)
            //    .ForeignKey("dbo.Website", t => t.BlogId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.AddressId)
            //    .Index(t => t.LocationId)
            //    .Index(t => t.BlogId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.TeacherService",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //            DisplayOrder = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.TeacherAccreditation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            TeacherId = c.Int(nullable: false),
            //            AccreditationBodyId = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Id, t.TeacherId, t.AccreditationBodyId })
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .ForeignKey("dbo.AccreditationBody", t => t.AccreditationBodyId, cascadeDelete: true)
            //    .Index(t => t.TeacherId)
            //    .Index(t => t.AccreditationBodyId);
            
            //CreateTable(
            //    "dbo.EntityHandle",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EntityId = c.Int(nullable: false),
            //            Name = c.String(maxLength: 100),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            EntityTypeId = c.Int(nullable: false),
            //            ImageId = c.Int(),
            //            OwnerId = c.Int(),
            //            ParentId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .ForeignKey("dbo.EntityHandle", t => t.ParentId)
            //    .Index(t => t.EntityTypeId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.OwnerId)
            //    .Index(t => t.ParentId);
            
            //CreateTable(
            //    "dbo.EntityType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 255),
            //            SystemName = c.String(nullable: false, maxLength: 255),
            //            DisplayName = c.String(nullable: false, maxLength: 100),
            //            DisplayPlural = c.String(nullable: false, maxLength: 100),
            //            Controller = c.String(maxLength: 100),
            //            IsProfessional = c.Boolean(nullable: false),
            //            IsFanable = c.Boolean(nullable: false),
            //            IsReviewable = c.Boolean(nullable: false),
            //            IsSearchable = c.Boolean(nullable: false),
            //            IsGalleryAssociate = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Fan",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            EntityHandleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.EntityHandle", t => t.EntityHandleId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.EntityHandleId);
            
            //CreateTable(
            //    "dbo.ActivityRepeatFrequency",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            IsSingle = c.Boolean(nullable: false),
            //            IsDaily = c.Boolean(nullable: false),
            //            IsWeekly = c.Boolean(nullable: false),
            //            IsFortnightly = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Wall",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.WallPost",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Content = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            OwnerId = c.Int(nullable: false),
            //            ActorHandleId = c.Int(nullable: false),
            //            WallId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .ForeignKey("dbo.EntityHandle", t => t.ActorHandleId)
            //    .ForeignKey("dbo.Wall", t => t.WallId, cascadeDelete: true)
            //    .Index(t => t.OwnerId)
            //    .Index(t => t.ActorHandleId)
            //    .Index(t => t.WallId);
            
            //CreateTable(
            //    "dbo.Group",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Description = c.String(),
            //            Public = c.Boolean(nullable: false),
            //            Professional = c.Boolean(nullable: false),
            //            ProfessionsRestricted = c.Boolean(nullable: false),
            //            MemberInvitationsPermitted = c.Boolean(nullable: false),
            //            MemberPostingPermitted = c.Boolean(nullable: false),
            //            MemberGalleriesPermitted = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            OwnerId = c.Int(),
            //            WallId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .ForeignKey("dbo.GroupWall", t => t.WallId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.OwnerId)
            //    .Index(t => t.WallId);
            
            //CreateTable(
            //    "dbo.GroupMember",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Administrator = c.Boolean(nullable: false),
            //            Confirmed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            GroupId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Group", t => t.GroupId, cascadeDelete: true)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.GroupId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Comment",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Content = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            OwnerId = c.Int(nullable: false),
            //            ActorHandleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .ForeignKey("dbo.EntityHandle", t => t.ActorHandleId)
            //    .Index(t => t.OwnerId)
            //    .Index(t => t.ActorHandleId);
            
            //CreateTable(
            //    "dbo.ActivityTeacher",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Confirmed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ActivityId = c.Int(nullable: false),
            //            TeacherId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Activity", t => t.ActivityId, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .Index(t => t.ActivityId)
            //    .Index(t => t.TeacherId);
            
            //CreateTable(
            //    "dbo.UserRole",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            RoleId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Role", t => t.RoleId)
            //    .ForeignKey("dbo.User", t => t.UserId)
            //    .Index(t => t.RoleId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Role",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Guid = c.Guid(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.NotificationPreferences",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EmailDigestEnabled = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            EmailDigestTimescaleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId)
            //    .ForeignKey("dbo.Timescale", t => t.EmailDigestTimescaleId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.EmailDigestTimescaleId);
            
            //CreateTable(
            //    "dbo.Timescale",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Tag = c.String(),
            //            Name = c.String(nullable: false),
            //            IsDay = c.Boolean(nullable: false),
            //            IsWeek = c.Boolean(nullable: false),
            //            IsMonth = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Accreditation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            AccreditationBodyId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AccreditationBody", t => t.AccreditationBodyId, cascadeDelete: true)
            //    .Index(t => t.AccreditationBodyId);
            
            //CreateTable(
            //    "dbo.GalleryAssociation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            GalleryId = c.Int(nullable: false),
            //            HandleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Gallery", t => t.GalleryId)
            //    .ForeignKey("dbo.EntityHandle", t => t.HandleId)
            //    .Index(t => t.GalleryId)
            //    .Index(t => t.HandleId);
            
            //CreateTable(
            //    "dbo.Gallery",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Public = c.Boolean(nullable: false),
            //            Open = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            CoverImageId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Image", t => t.CoverImageId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.CoverImageId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.GalleryEntry",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Promoted = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            GalleryId = c.Int(nullable: false),
            //            ImageId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Gallery", t => t.GalleryId, cascadeDelete: true)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .Index(t => t.GalleryId)
            //    .Index(t => t.ImageId);
            
            //CreateTable(
            //    "dbo.FederatedLogin",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Identifier = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            ProviderId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.FederatedLoginProvider", t => t.ProviderId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.ProviderId);
            
            //CreateTable(
            //    "dbo.FederatedLoginProvider",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Tag = c.String(nullable: false),
            //            Name = c.String(nullable: false),
            //            IsFacebook = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.GlossaryEntry",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Term = c.String(nullable: false, maxLength: 100),
            //            Definition = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Invitation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            RequestIdentifier = c.String(maxLength: 255),
            //            RecipientIdentifier = c.String(nullable: false, maxLength: 255),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            LoginProviderId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Profile", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.FederatedLoginProvider", t => t.LoginProviderId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.LoginProviderId);
            
            //CreateTable(
            //    "dbo.NewsStory",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EntityId = c.Int(),
            //            Text = c.String(maxLength: 4000),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //            SubjectHandleId = c.Int(nullable: false),
            //            ActorHandleId = c.Int(),
            //            TargetHandleId = c.Int(),
            //            EntityTypeId = c.Int(),
            //            InverseId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.NewsStoryType", t => t.TypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.EntityHandle", t => t.SubjectHandleId)
            //    .ForeignKey("dbo.EntityHandle", t => t.ActorHandleId)
            //    .ForeignKey("dbo.EntityHandle", t => t.TargetHandleId)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId)
            //    .ForeignKey("dbo.NewsStory", t => t.InverseId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.SubjectHandleId)
            //    .Index(t => t.ActorHandleId)
            //    .Index(t => t.TargetHandleId)
            //    .Index(t => t.EntityTypeId)
            //    .Index(t => t.InverseId);
            
            //CreateTable(
            //    "dbo.NewsStoryType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Tag = c.String(nullable: false, maxLength: 255),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Vain = c.Boolean(nullable: false),
            //            Deduped = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.NewsSubscription",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            SubscriberProfileId = c.Int(nullable: false),
            //            SubjectHandleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Profile", t => t.SubscriberProfileId)
            //    .ForeignKey("dbo.EntityHandle", t => t.SubjectHandleId)
            //    .Index(t => t.SubscriberProfileId)
            //    .Index(t => t.SubjectHandleId);
            
            //CreateTable(
            //    "dbo.NotificationCategory",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.NotificationType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Tag = c.String(nullable: false),
            //            Message = c.String(nullable: false),
            //            Icon = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            CategoryId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.NotificationCategory", t => t.CategoryId)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.NotificationDelivery",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Read = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            Notificationid = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Notification", t => t.Notificationid, cascadeDelete: true)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.Notificationid)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Notification",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Message = c.String(),
            //            EntityId = c.Int(),
            //            Broadcast = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //            ActorHandleId = c.Int(),
            //            SubjectHandleId = c.Int(),
            //            ContextHandleId = c.Int(),
            //            EntityTypeId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.NotificationType", t => t.TypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.EntityHandle", t => t.ActorHandleId)
            //    .ForeignKey("dbo.EntityHandle", t => t.SubjectHandleId)
            //    .ForeignKey("dbo.EntityHandle", t => t.ContextHandleId)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.ActorHandleId)
            //    .Index(t => t.SubjectHandleId)
            //    .Index(t => t.ContextHandleId)
            //    .Index(t => t.EntityTypeId);
            
            //CreateTable(
            //    "dbo.NotificationCategoryPreferences",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            NotificationEnabled = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            NotificationCategoryId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.NotificationCategory", t => t.NotificationCategoryId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.NotificationCategoryId);
            
            //CreateTable(
            //    "dbo.PasswordLogin",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Username = c.String(nullable: false, maxLength: 254),
            //            PasswordHash = c.String(nullable: false, maxLength: 60),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.PasswordResetAction",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ClientAddress = c.String(nullable: false, maxLength: 39, unicode: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            RequestId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.PasswordResetRequest", t => t.RequestId)
            //    .Index(t => t.RequestId);
            
            //CreateTable(
            //    "dbo.PasswordResetRequest",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Key = c.String(nullable: false, maxLength: 50, unicode: false),
            //            ClientAddress = c.String(nullable: false, maxLength: 39, unicode: false),
            //            Completed = c.Boolean(nullable: false),
            //            ExpiryTime = c.DateTime(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            LoginId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.PasswordLogin", t => t.LoginId, cascadeDelete: true)
            //    .Index(t => t.LoginId);
            
            //CreateTable(
            //    "dbo.PoseCategory",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Caption = c.String(nullable: false, maxLength: 100),
            //            SortKey = c.Int(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Pose",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EnglishName = c.String(nullable: false, maxLength: 100),
            //            SanskritName = c.String(nullable: false, maxLength: 100),
            //            Roots = c.String(nullable: false),
            //            Pronounciation = c.String(nullable: false, maxLength: 100),
            //            Instructions = c.String(nullable: false),
            //            Effects = c.String(nullable: false),
            //            Tips = c.String(nullable: false),
            //            Indications = c.String(nullable: false),
            //            Contraindications = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            AbilityLevelId = c.Int(nullable: false),
            //            ImageId = c.Int(),
            //            VideoId = c.Int(),
            //            CategoryId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AbilityLevel", t => t.AbilityLevelId, cascadeDelete: true)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Video", t => t.VideoId)
            //    .ForeignKey("dbo.PoseCategory", t => t.CategoryId, cascadeDelete: true)
            //    .Index(t => t.AbilityLevelId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.VideoId)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.Video",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Identifier = c.String(),
            //            Description = c.String(),
            //            Length = c.String(),
            //            IsClass = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            AbilityLevelId = c.Int(),
            //            TypeId = c.Int(nullable: false),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AbilityLevel", t => t.AbilityLevelId)
            //    .ForeignKey("dbo.VideoType", t => t.TypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.AbilityLevelId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.VideoType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            UrlPattern = c.String(),
            //            EmbedHtml = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.VideoAssociation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            VideoId = c.Int(nullable: false),
            //            HandleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Video", t => t.VideoId)
            //    .ForeignKey("dbo.EntityHandle", t => t.HandleId)
            //    .Index(t => t.VideoId)
            //    .Index(t => t.HandleId);
            
            //CreateTable(
            //    "dbo.PrivateConversation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.PrivateMessage",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Content = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ConversationId = c.Int(nullable: false),
            //            SenderProfileId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.PrivateConversation", t => t.ConversationId, cascadeDelete: true)
            //    .ForeignKey("dbo.Profile", t => t.SenderProfileId, cascadeDelete: true)
            //    .Index(t => t.ConversationId)
            //    .Index(t => t.SenderProfileId);
            
            //CreateTable(
            //    "dbo.PrivateMessageDelivery",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Read = c.Boolean(nullable: false),
            //            Hidden = c.Boolean(nullable: false),
            //            ReadTime = c.DateTime(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            RecipientProfileId = c.Int(nullable: false),
            //            MessageId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Profile", t => t.RecipientProfileId)
            //    .ForeignKey("dbo.PrivateMessage", t => t.MessageId)
            //    .Index(t => t.RecipientProfileId)
            //    .Index(t => t.MessageId);
            
            //CreateTable(
            //    "dbo.Quote",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Author = c.String(nullable: false, maxLength: 100),
            //            Quotation = c.String(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Request",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EntityId = c.Int(nullable: false),
            //            Completed = c.Boolean(nullable: false),
            //            CompletionTime = c.DateTime(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //            ActorHandleId = c.Int(nullable: false),
            //            SubjectHandleId = c.Int(),
            //            ContextHandleId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .ForeignKey("dbo.RequestType", t => t.TypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.EntityHandle", t => t.ActorHandleId, cascadeDelete: true)
            //    .ForeignKey("dbo.EntityHandle", t => t.SubjectHandleId)
            //    .ForeignKey("dbo.EntityHandle", t => t.ContextHandleId)
            //    .Index(t => t.UserId)
            //    .Index(t => t.TypeId)
            //    .Index(t => t.ActorHandleId)
            //    .Index(t => t.SubjectHandleId)
            //    .Index(t => t.ContextHandleId);
            
            //CreateTable(
            //    "dbo.RequestType",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Tag = c.String(nullable: false),
            //            Message = c.String(nullable: false),
            //            Icon = c.String(nullable: false),
            //            CompletionHandlerTypeName = c.String(nullable: false),
            //            IsFriend = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.ReviewExperience",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FilterKey = c.String(maxLength: 100),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            EntityTypeId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId)
            //    .Index(t => t.EntityTypeId);
            
            //CreateTable(
            //    "dbo.Review",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Headline = c.String(nullable: false, maxLength: 100),
            //            Body = c.String(nullable: false),
            //            Rating = c.Decimal(nullable: false, precision: 2, scale: 1),
            //            HelpfulCount = c.Int(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            AuthorHandleId = c.Int(nullable: false),
            //            SubjectHandleId = c.Int(nullable: false),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityHandle", t => t.AuthorHandleId)
            //    .ForeignKey("dbo.EntityHandle", t => t.SubjectHandleId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.AuthorHandleId)
            //    .Index(t => t.SubjectHandleId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.ReviewRating",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ReviewId = c.Int(nullable: false),
            //            SubjectId = c.Int(nullable: false),
            //            Score = c.Decimal(nullable: false, precision: 2, scale: 1),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.Id, t.ReviewId, t.SubjectId })
            //    .ForeignKey("dbo.ReviewRatingSubject", t => t.SubjectId, cascadeDelete: true)
            //    .ForeignKey("dbo.Review", t => t.ReviewId, cascadeDelete: true)
            //    .Index(t => t.SubjectId)
            //    .Index(t => t.ReviewId);
            
            //CreateTable(
            //    "dbo.ReviewRatingSubject",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            FilterKey = c.String(maxLength: 100),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            EntityTypeId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId)
            //    .Index(t => t.EntityTypeId);
            
            //CreateTable(
            //    "dbo.ReviewFeedbackResponse",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Helpful = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ReviewId = c.Int(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Review", t => t.ReviewId)
            //    .ForeignKey("dbo.User", t => t.UserId)
            //    .Index(t => t.ReviewId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.SearchRecord",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            EntityId = c.Int(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Description = c.String(nullable: false, maxLength: 500),
            //            City = c.String(maxLength: 100),
            //            Area = c.String(maxLength: 100),
            //            Country = c.String(maxLength: 100),
            //            Postcode = c.String(maxLength: 20),
            //            Date = c.DateTime(),
            //            Location = c.Geography(),
            //            Styles = c.String(maxLength: 1000),
            //            Rating = c.Decimal(precision: 2, scale: 1),
            //            Stubbed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            EntityTypeId = c.Int(nullable: false),
            //            VenueTypeId = c.Int(),
            //            ImageId = c.Int(),
            //            ParentId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.VenueType", t => t.VenueTypeId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.SearchRecord", t => t.ParentId)
            //    .Index(t => t.EntityTypeId)
            //    .Index(t => t.VenueTypeId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ParentId);
            
            //CreateTable(
            //    "dbo.StyleOrganisation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Founder = c.String(maxLength: 100),
            //            FoundedYear = c.Int(nullable: false),
            //            EmailAddress = c.String(),
            //            Telephone = c.String(),
            //            Description = c.String(),
            //            Affiliations = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //            WebsiteId = c.Int(),
            //            LocationId = c.Int(nullable: false),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId)
            //    .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.StyleId)
            //    .Index(t => t.WebsiteId)
            //    .Index(t => t.LocationId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.TeacherTrainingCourse",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Notes = c.String(),
            //            Duration = c.String(maxLength: 100),
            //            StartDate = c.DateTime(),
            //            FinishDate = c.DateTime(),
            //            PreRequisites = c.String(),
            //            Accreditation = c.String(),
            //            Active = c.Boolean(nullable: false),
            //            ImageCourtesyOf = c.String(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            OrganisationId = c.Int(nullable: false),
            //            WebsiteId = c.Int(),
            //            ImageId = c.Int(),
            //            StyleId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.TeacherTrainingOrganisation", t => t.OrganisationId, cascadeDelete: true)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Style", t => t.StyleId)
            //    .Index(t => t.OrganisationId)
            //    .Index(t => t.WebsiteId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.TeacherTrainingOrganisation",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 100),
            //            Founder = c.String(maxLength: 100),
            //            Stubbed = c.Boolean(nullable: false),
            //            Active = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            ImageId = c.Int(),
            //            ProfileBannerImageId = c.Int(),
            //            AddressId = c.Int(),
            //            WebsiteId = c.Int(),
            //            OwnerId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Image", t => t.ImageId)
            //    .ForeignKey("dbo.Image", t => t.ProfileBannerImageId)
            //    .ForeignKey("dbo.Address", t => t.AddressId)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId)
            //    .ForeignKey("dbo.User", t => t.OwnerId)
            //    .Index(t => t.ImageId)
            //    .Index(t => t.ProfileBannerImageId)
            //    .Index(t => t.AddressId)
            //    .Index(t => t.WebsiteId)
            //    .Index(t => t.OwnerId);
            
            //CreateTable(
            //    "dbo.TeacherTrainingCourseTeacher",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Confirmed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            CourseId = c.Int(nullable: false),
            //            TeacherId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.TeacherTrainingCourse", t => t.CourseId, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .Index(t => t.CourseId)
            //    .Index(t => t.TeacherId);
            
            //CreateTable(
            //    "dbo.TeacherTrainingCourseVenue",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Confirmed = c.Boolean(nullable: false),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            CourseId = c.Int(nullable: false),
            //            VenueId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.TeacherTrainingCourse", t => t.CourseId, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .Index(t => t.CourseId)
            //    .Index(t => t.VenueId);
            
            //CreateTable(
            //    "dbo.UserActivationRequest",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Key = c.String(nullable: false, maxLength: 100, unicode: false),
            //            Completed = c.Boolean(nullable: false),
            //            CompletionTime = c.DateTime(),
            //            CreationTime = c.DateTime(nullable: false),
            //            ModificationTime = c.DateTime(nullable: false),
            //            UserId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.ProfileWebsiteLink",
            //    c => new
            //        {
            //            ProfileId = c.Int(nullable: false),
            //            WebsiteId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ProfileId, t.WebsiteId })
            //    .ForeignKey("dbo.Profile", t => t.ProfileId, cascadeDelete: true)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId, cascadeDelete: true)
            //    .Index(t => t.ProfileId)
            //    .Index(t => t.WebsiteId);
            
            //CreateTable(
            //    "dbo.VenueWebsite",
            //    c => new
            //        {
            //            VenueId = c.Int(nullable: false),
            //            WebsiteId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.VenueId, t.WebsiteId })
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId, cascadeDelete: true)
            //    .Index(t => t.VenueId)
            //    .Index(t => t.WebsiteId);
            
            //CreateTable(
            //    "dbo.StyleTraitLink",
            //    c => new
            //        {
            //            StyleId = c.Int(nullable: false),
            //            TraitId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.StyleId, t.TraitId })
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .ForeignKey("dbo.StyleTrait", t => t.TraitId, cascadeDelete: true)
            //    .Index(t => t.StyleId)
            //    .Index(t => t.TraitId);
            
            //CreateTable(
            //    "dbo.VenueStyleLink",
            //    c => new
            //        {
            //            VenueId = c.Int(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.VenueId, t.StyleId })
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .Index(t => t.VenueId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.VenueServiceLink",
            //    c => new
            //        {
            //            VenueId = c.Int(nullable: false),
            //            ServiceId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.VenueId, t.ServiceId })
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .ForeignKey("dbo.VenueService", t => t.ServiceId, cascadeDelete: true)
            //    .Index(t => t.VenueId)
            //    .Index(t => t.ServiceId);
            
            //CreateTable(
            //    "dbo.VenueAmenityLink",
            //    c => new
            //        {
            //            VenueId = c.Int(nullable: false),
            //            AmenityId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.VenueId, t.AmenityId })
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .ForeignKey("dbo.VenueAmenity", t => t.AmenityId, cascadeDelete: true)
            //    .Index(t => t.VenueId)
            //    .Index(t => t.AmenityId);
            
            //CreateTable(
            //    "dbo.TeacherWebsite",
            //    c => new
            //        {
            //            TeacherId = c.Int(nullable: false),
            //            WebsiteId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.TeacherId, t.WebsiteId })
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .ForeignKey("dbo.Website", t => t.WebsiteId, cascadeDelete: true)
            //    .Index(t => t.TeacherId)
            //    .Index(t => t.WebsiteId);
            
            //CreateTable(
            //    "dbo.TeacherStyleLink",
            //    c => new
            //        {
            //            TeacherId = c.Int(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.TeacherId, t.StyleId })
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .Index(t => t.TeacherId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.TeacherServiceLink",
            //    c => new
            //        {
            //            TeacherId = c.Int(nullable: false),
            //            ServiceId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.TeacherId, t.ServiceId })
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .ForeignKey("dbo.TeacherService", t => t.ServiceId, cascadeDelete: true)
            //    .Index(t => t.TeacherId)
            //    .Index(t => t.ServiceId);
            
            //CreateTable(
            //    "dbo.GroupStyle",
            //    c => new
            //        {
            //            GroupId = c.Int(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.GroupId, t.StyleId })
            //    .ForeignKey("dbo.Group", t => t.GroupId, cascadeDelete: true)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .Index(t => t.GroupId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.GroupProfession",
            //    c => new
            //        {
            //            GroupId = c.Int(nullable: false),
            //            EntityTypeId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.GroupId, t.EntityTypeId })
            //    .ForeignKey("dbo.Group", t => t.GroupId, cascadeDelete: true)
            //    .ForeignKey("dbo.EntityType", t => t.EntityTypeId, cascadeDelete: true)
            //    .Index(t => t.GroupId)
            //    .Index(t => t.EntityTypeId);
            
            //CreateTable(
            //    "dbo.WallPostComment",
            //    c => new
            //        {
            //            WallId = c.Int(nullable: false),
            //            CommentId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.WallId, t.CommentId })
            //    .ForeignKey("dbo.WallPost", t => t.WallId, cascadeDelete: true)
            //    .ForeignKey("dbo.Comment", t => t.CommentId, cascadeDelete: true)
            //    .Index(t => t.WallId)
            //    .Index(t => t.CommentId);
            
            //CreateTable(
            //    "dbo.ActivityStyle",
            //    c => new
            //        {
            //            ActivityId = c.Int(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ActivityId, t.StyleId })
            //    .ForeignKey("dbo.Activity", t => t.ActivityId, cascadeDelete: true)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .Index(t => t.ActivityId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.PrivateConversationParticipant",
            //    c => new
            //        {
            //            ConversationId = c.Int(nullable: false),
            //            ProfileId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ConversationId, t.ProfileId })
            //    .ForeignKey("dbo.PrivateConversation", t => t.ConversationId, cascadeDelete: true)
            //    .ForeignKey("dbo.Profile", t => t.ProfileId, cascadeDelete: true)
            //    .Index(t => t.ConversationId)
            //    .Index(t => t.ProfileId);
            
            //CreateTable(
            //    "dbo.ReviewExperienceLink",
            //    c => new
            //        {
            //            ReviewId = c.Int(nullable: false),
            //            TypeId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ReviewId, t.TypeId })
            //    .ForeignKey("dbo.Review", t => t.ReviewId, cascadeDelete: true)
            //    .ForeignKey("dbo.ReviewExperience", t => t.TypeId, cascadeDelete: true)
            //    .Index(t => t.ReviewId)
            //    .Index(t => t.TypeId);
            
            //CreateTable(
            //    "dbo.ReviewComment",
            //    c => new
            //        {
            //            ReviewId = c.Int(nullable: false),
            //            CommentId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ReviewId, t.CommentId })
            //    .ForeignKey("dbo.Review", t => t.ReviewId, cascadeDelete: true)
            //    .ForeignKey("dbo.Comment", t => t.CommentId, cascadeDelete: true)
            //    .Index(t => t.ReviewId)
            //    .Index(t => t.CommentId);
            
            //CreateTable(
            //    "dbo.SearchRecordStyleLink",
            //    c => new
            //        {
            //            RecordId = c.Int(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.RecordId, t.StyleId })
            //    .ForeignKey("dbo.SearchRecord", t => t.RecordId, cascadeDelete: true)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .Index(t => t.RecordId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.SearchRecordTeacherLink",
            //    c => new
            //        {
            //            RecordId = c.Int(nullable: false),
            //            TeacherId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.RecordId, t.TeacherId })
            //    .ForeignKey("dbo.SearchRecord", t => t.RecordId, cascadeDelete: true)
            //    .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
            //    .Index(t => t.RecordId)
            //    .Index(t => t.TeacherId);
            
            //CreateTable(
            //    "dbo.SearchRecordTeacherServiceLink",
            //    c => new
            //        {
            //            RecordId = c.Int(nullable: false),
            //            TeacherServiceId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.RecordId, t.TeacherServiceId })
            //    .ForeignKey("dbo.SearchRecord", t => t.RecordId, cascadeDelete: true)
            //    .ForeignKey("dbo.TeacherService", t => t.TeacherServiceId, cascadeDelete: true)
            //    .Index(t => t.RecordId)
            //    .Index(t => t.TeacherServiceId);
            
            //CreateTable(
            //    "dbo.SearchRecordVenueLink",
            //    c => new
            //        {
            //            RecordId = c.Int(nullable: false),
            //            VenueId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.RecordId, t.VenueId })
            //    .ForeignKey("dbo.SearchRecord", t => t.RecordId, cascadeDelete: true)
            //    .ForeignKey("dbo.Venue", t => t.VenueId, cascadeDelete: true)
            //    .Index(t => t.RecordId)
            //    .Index(t => t.VenueId);
            
            //CreateTable(
            //    "dbo.SearchRecordVenueServiceLink",
            //    c => new
            //        {
            //            RecordId = c.Int(nullable: false),
            //            VenueServiceId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.RecordId, t.VenueServiceId })
            //    .ForeignKey("dbo.SearchRecord", t => t.RecordId, cascadeDelete: true)
            //    .ForeignKey("dbo.VenueService", t => t.VenueServiceId, cascadeDelete: true)
            //    .Index(t => t.RecordId)
            //    .Index(t => t.VenueServiceId);
            
            //CreateTable(
            //    "dbo.TeacherTrainingOrganisationStyleLink",
            //    c => new
            //        {
            //            OrganisationId = c.Int(nullable: false),
            //            StyleId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.OrganisationId, t.StyleId })
            //    .ForeignKey("dbo.TeacherTrainingOrganisation", t => t.OrganisationId, cascadeDelete: true)
            //    .ForeignKey("dbo.Style", t => t.StyleId, cascadeDelete: true)
            //    .Index(t => t.OrganisationId)
            //    .Index(t => t.StyleId);
            
            //CreateTable(
            //    "dbo.TeacherTrainingCourseAccreditations",
            //    c => new
            //        {
            //            CourseId = c.Int(nullable: false),
            //            AccreditationBodyId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.CourseId, t.AccreditationBodyId })
            //    .ForeignKey("dbo.TeacherTrainingCourse", t => t.CourseId, cascadeDelete: true)
            //    .ForeignKey("dbo.AccreditationBody", t => t.AccreditationBodyId, cascadeDelete: true)
            //    .Index(t => t.CourseId)
            //    .Index(t => t.AccreditationBodyId);
            
            //CreateTable(
            //    "dbo.ActivityWall",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Wall", t => t.Id)
            //    .Index(t => t.Id);
            
            //CreateTable(
            //    "dbo.GroupWall",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Wall", t => t.Id)
            //    .Index(t => t.Id);
            
            //CreateTable(
            //    "dbo.ProfileWall",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Wall", t => t.Id)
            //    .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProfileWall", new[] { "Id" });
            DropIndex("dbo.GroupWall", new[] { "Id" });
            DropIndex("dbo.ActivityWall", new[] { "Id" });
            DropIndex("dbo.TeacherTrainingCourseAccreditations", new[] { "AccreditationBodyId" });
            DropIndex("dbo.TeacherTrainingCourseAccreditations", new[] { "CourseId" });
            DropIndex("dbo.TeacherTrainingOrganisationStyleLink", new[] { "StyleId" });
            DropIndex("dbo.TeacherTrainingOrganisationStyleLink", new[] { "OrganisationId" });
            DropIndex("dbo.SearchRecordVenueServiceLink", new[] { "VenueServiceId" });
            DropIndex("dbo.SearchRecordVenueServiceLink", new[] { "RecordId" });
            DropIndex("dbo.SearchRecordVenueLink", new[] { "VenueId" });
            DropIndex("dbo.SearchRecordVenueLink", new[] { "RecordId" });
            DropIndex("dbo.SearchRecordTeacherServiceLink", new[] { "TeacherServiceId" });
            DropIndex("dbo.SearchRecordTeacherServiceLink", new[] { "RecordId" });
            DropIndex("dbo.SearchRecordTeacherLink", new[] { "TeacherId" });
            DropIndex("dbo.SearchRecordTeacherLink", new[] { "RecordId" });
            DropIndex("dbo.SearchRecordStyleLink", new[] { "StyleId" });
            DropIndex("dbo.SearchRecordStyleLink", new[] { "RecordId" });
            DropIndex("dbo.ReviewComment", new[] { "CommentId" });
            DropIndex("dbo.ReviewComment", new[] { "ReviewId" });
            DropIndex("dbo.ReviewExperienceLink", new[] { "TypeId" });
            DropIndex("dbo.ReviewExperienceLink", new[] { "ReviewId" });
            DropIndex("dbo.PrivateConversationParticipant", new[] { "ProfileId" });
            DropIndex("dbo.PrivateConversationParticipant", new[] { "ConversationId" });
            DropIndex("dbo.ActivityStyle", new[] { "StyleId" });
            DropIndex("dbo.ActivityStyle", new[] { "ActivityId" });
            DropIndex("dbo.WallPostComment", new[] { "CommentId" });
            DropIndex("dbo.WallPostComment", new[] { "WallId" });
            DropIndex("dbo.GroupProfession", new[] { "EntityTypeId" });
            DropIndex("dbo.GroupProfession", new[] { "GroupId" });
            DropIndex("dbo.GroupStyle", new[] { "StyleId" });
            DropIndex("dbo.GroupStyle", new[] { "GroupId" });
            DropIndex("dbo.TeacherServiceLink", new[] { "ServiceId" });
            DropIndex("dbo.TeacherServiceLink", new[] { "TeacherId" });
            DropIndex("dbo.TeacherStyleLink", new[] { "StyleId" });
            DropIndex("dbo.TeacherStyleLink", new[] { "TeacherId" });
            DropIndex("dbo.TeacherWebsite", new[] { "WebsiteId" });
            DropIndex("dbo.TeacherWebsite", new[] { "TeacherId" });
            DropIndex("dbo.VenueAmenityLink", new[] { "AmenityId" });
            DropIndex("dbo.VenueAmenityLink", new[] { "VenueId" });
            DropIndex("dbo.VenueServiceLink", new[] { "ServiceId" });
            DropIndex("dbo.VenueServiceLink", new[] { "VenueId" });
            DropIndex("dbo.VenueStyleLink", new[] { "StyleId" });
            DropIndex("dbo.VenueStyleLink", new[] { "VenueId" });
            DropIndex("dbo.StyleTraitLink", new[] { "TraitId" });
            DropIndex("dbo.StyleTraitLink", new[] { "StyleId" });
            DropIndex("dbo.VenueWebsite", new[] { "WebsiteId" });
            DropIndex("dbo.VenueWebsite", new[] { "VenueId" });
            DropIndex("dbo.ProfileWebsiteLink", new[] { "WebsiteId" });
            DropIndex("dbo.ProfileWebsiteLink", new[] { "ProfileId" });
            DropIndex("dbo.UserActivationRequest", new[] { "UserId" });
            DropIndex("dbo.TeacherTrainingCourseVenue", new[] { "VenueId" });
            DropIndex("dbo.TeacherTrainingCourseVenue", new[] { "CourseId" });
            DropIndex("dbo.TeacherTrainingCourseTeacher", new[] { "TeacherId" });
            DropIndex("dbo.TeacherTrainingCourseTeacher", new[] { "CourseId" });
            DropIndex("dbo.TeacherTrainingOrganisation", new[] { "OwnerId" });
            DropIndex("dbo.TeacherTrainingOrganisation", new[] { "WebsiteId" });
            DropIndex("dbo.TeacherTrainingOrganisation", new[] { "AddressId" });
            DropIndex("dbo.TeacherTrainingOrganisation", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.TeacherTrainingOrganisation", new[] { "ImageId" });
            DropIndex("dbo.TeacherTrainingCourse", new[] { "StyleId" });
            DropIndex("dbo.TeacherTrainingCourse", new[] { "ImageId" });
            DropIndex("dbo.TeacherTrainingCourse", new[] { "WebsiteId" });
            DropIndex("dbo.TeacherTrainingCourse", new[] { "OrganisationId" });
            DropIndex("dbo.StyleOrganisation", new[] { "OwnerId" });
            DropIndex("dbo.StyleOrganisation", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.StyleOrganisation", new[] { "ImageId" });
            DropIndex("dbo.StyleOrganisation", new[] { "LocationId" });
            DropIndex("dbo.StyleOrganisation", new[] { "WebsiteId" });
            DropIndex("dbo.StyleOrganisation", new[] { "StyleId" });
            DropIndex("dbo.SearchRecord", new[] { "ParentId" });
            DropIndex("dbo.SearchRecord", new[] { "ImageId" });
            DropIndex("dbo.SearchRecord", new[] { "VenueTypeId" });
            DropIndex("dbo.SearchRecord", new[] { "EntityTypeId" });
            DropIndex("dbo.ReviewFeedbackResponse", new[] { "UserId" });
            DropIndex("dbo.ReviewFeedbackResponse", new[] { "ReviewId" });
            DropIndex("dbo.ReviewRatingSubject", new[] { "EntityTypeId" });
            DropIndex("dbo.ReviewRating", new[] { "ReviewId" });
            DropIndex("dbo.ReviewRating", new[] { "SubjectId" });
            DropIndex("dbo.Review", new[] { "OwnerId" });
            DropIndex("dbo.Review", new[] { "SubjectHandleId" });
            DropIndex("dbo.Review", new[] { "AuthorHandleId" });
            DropIndex("dbo.ReviewExperience", new[] { "EntityTypeId" });
            DropIndex("dbo.Request", new[] { "ContextHandleId" });
            DropIndex("dbo.Request", new[] { "SubjectHandleId" });
            DropIndex("dbo.Request", new[] { "ActorHandleId" });
            DropIndex("dbo.Request", new[] { "TypeId" });
            DropIndex("dbo.Request", new[] { "UserId" });
            DropIndex("dbo.PrivateMessageDelivery", new[] { "MessageId" });
            DropIndex("dbo.PrivateMessageDelivery", new[] { "RecipientProfileId" });
            DropIndex("dbo.PrivateMessage", new[] { "SenderProfileId" });
            DropIndex("dbo.PrivateMessage", new[] { "ConversationId" });
            DropIndex("dbo.VideoAssociation", new[] { "HandleId" });
            DropIndex("dbo.VideoAssociation", new[] { "VideoId" });
            DropIndex("dbo.Video", new[] { "OwnerId" });
            DropIndex("dbo.Video", new[] { "TypeId" });
            DropIndex("dbo.Video", new[] { "AbilityLevelId" });
            DropIndex("dbo.Pose", new[] { "CategoryId" });
            DropIndex("dbo.Pose", new[] { "VideoId" });
            DropIndex("dbo.Pose", new[] { "ImageId" });
            DropIndex("dbo.Pose", new[] { "AbilityLevelId" });
            DropIndex("dbo.PasswordResetRequest", new[] { "LoginId" });
            DropIndex("dbo.PasswordResetAction", new[] { "RequestId" });
            DropIndex("dbo.PasswordLogin", new[] { "UserId" });
            DropIndex("dbo.NotificationCategoryPreferences", new[] { "NotificationCategoryId" });
            DropIndex("dbo.NotificationCategoryPreferences", new[] { "UserId" });
            DropIndex("dbo.Notification", new[] { "EntityTypeId" });
            DropIndex("dbo.Notification", new[] { "ContextHandleId" });
            DropIndex("dbo.Notification", new[] { "SubjectHandleId" });
            DropIndex("dbo.Notification", new[] { "ActorHandleId" });
            DropIndex("dbo.Notification", new[] { "TypeId" });
            DropIndex("dbo.NotificationDelivery", new[] { "UserId" });
            DropIndex("dbo.NotificationDelivery", new[] { "Notificationid" });
            DropIndex("dbo.NotificationType", new[] { "CategoryId" });
            DropIndex("dbo.NewsSubscription", new[] { "SubjectHandleId" });
            DropIndex("dbo.NewsSubscription", new[] { "SubscriberProfileId" });
            DropIndex("dbo.NewsStory", new[] { "InverseId" });
            DropIndex("dbo.NewsStory", new[] { "EntityTypeId" });
            DropIndex("dbo.NewsStory", new[] { "TargetHandleId" });
            DropIndex("dbo.NewsStory", new[] { "ActorHandleId" });
            DropIndex("dbo.NewsStory", new[] { "SubjectHandleId" });
            DropIndex("dbo.NewsStory", new[] { "TypeId" });
            DropIndex("dbo.Invitation", new[] { "LoginProviderId" });
            DropIndex("dbo.Invitation", new[] { "UserId" });
            DropIndex("dbo.FederatedLogin", new[] { "ProviderId" });
            DropIndex("dbo.FederatedLogin", new[] { "UserId" });
            DropIndex("dbo.GalleryEntry", new[] { "ImageId" });
            DropIndex("dbo.GalleryEntry", new[] { "GalleryId" });
            DropIndex("dbo.Gallery", new[] { "OwnerId" });
            DropIndex("dbo.Gallery", new[] { "CoverImageId" });
            DropIndex("dbo.GalleryAssociation", new[] { "HandleId" });
            DropIndex("dbo.GalleryAssociation", new[] { "GalleryId" });
            DropIndex("dbo.Accreditation", new[] { "AccreditationBodyId" });
            DropIndex("dbo.NotificationPreferences", new[] { "EmailDigestTimescaleId" });
            DropIndex("dbo.NotificationPreferences", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.ActivityTeacher", new[] { "TeacherId" });
            DropIndex("dbo.ActivityTeacher", new[] { "ActivityId" });
            DropIndex("dbo.Comment", new[] { "ActorHandleId" });
            DropIndex("dbo.Comment", new[] { "OwnerId" });
            DropIndex("dbo.GroupMember", new[] { "UserId" });
            DropIndex("dbo.GroupMember", new[] { "GroupId" });
            DropIndex("dbo.Group", new[] { "WallId" });
            DropIndex("dbo.Group", new[] { "OwnerId" });
            DropIndex("dbo.Group", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.Group", new[] { "ImageId" });
            DropIndex("dbo.WallPost", new[] { "WallId" });
            DropIndex("dbo.WallPost", new[] { "ActorHandleId" });
            DropIndex("dbo.WallPost", new[] { "OwnerId" });
            DropIndex("dbo.Fan", new[] { "EntityHandleId" });
            DropIndex("dbo.Fan", new[] { "UserId" });
            DropIndex("dbo.EntityHandle", new[] { "ParentId" });
            DropIndex("dbo.EntityHandle", new[] { "OwnerId" });
            DropIndex("dbo.EntityHandle", new[] { "ImageId" });
            DropIndex("dbo.EntityHandle", new[] { "EntityTypeId" });
            DropIndex("dbo.TeacherAccreditation", new[] { "AccreditationBodyId" });
            DropIndex("dbo.TeacherAccreditation", new[] { "TeacherId" });
            DropIndex("dbo.Teacher", new[] { "OwnerId" });
            DropIndex("dbo.Teacher", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.Teacher", new[] { "ImageId" });
            DropIndex("dbo.Teacher", new[] { "BlogId" });
            DropIndex("dbo.Teacher", new[] { "LocationId" });
            DropIndex("dbo.Teacher", new[] { "AddressId" });
            DropIndex("dbo.TeacherPlacement", new[] { "VenueId" });
            DropIndex("dbo.TeacherPlacement", new[] { "TeacherId" });
            DropIndex("dbo.StyleTrait", new[] { "GroupId" });
            DropIndex("dbo.Style", new[] { "OwnerId" });
            DropIndex("dbo.Style", new[] { "WebsiteId" });
            DropIndex("dbo.Style", new[] { "DirectoryImageId" });
            DropIndex("dbo.Style", new[] { "ImageId" });
            DropIndex("dbo.Address", new[] { "CountryId" });
            DropIndex("dbo.Venue", new[] { "OwnerId" });
            DropIndex("dbo.Venue", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.Venue", new[] { "ImageId" });
            DropIndex("dbo.Venue", new[] { "BlogId" });
            DropIndex("dbo.Venue", new[] { "AddressId" });
            DropIndex("dbo.Venue", new[] { "TypeId" });
            DropIndex("dbo.Activity", new[] { "OwnerId" });
            DropIndex("dbo.Activity", new[] { "WallId" });
            DropIndex("dbo.Activity", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.Activity", new[] { "ImageId" });
            DropIndex("dbo.Activity", new[] { "RepeatFrequencyId" });
            DropIndex("dbo.Activity", new[] { "AbilityLevelId" });
            DropIndex("dbo.Activity", new[] { "PromoterHandleId" });
            DropIndex("dbo.Activity", new[] { "VenueId" });
            DropIndex("dbo.Activity", new[] { "TypeId" });
            DropIndex("dbo.ActivityAttendee", new[] { "ProfileId" });
            DropIndex("dbo.ActivityAttendee", new[] { "ActivityId" });
            DropIndex("dbo.Friendship", new[] { "InverseId" });
            DropIndex("dbo.Friendship", new[] { "FriendId" });
            DropIndex("dbo.Friendship", new[] { "ProfileId" });
            DropIndex("dbo.ProfileAnswer", new[] { "ProfileId" });
            DropIndex("dbo.ProfileAnswer", new[] { "QuestionId" });
            DropIndex("dbo.Profile", new[] { "OwnerId" });
            DropIndex("dbo.Profile", new[] { "WallId" });
            DropIndex("dbo.Profile", new[] { "LocationId" });
            DropIndex("dbo.Profile", new[] { "ProfileBannerImageId" });
            DropIndex("dbo.Profile", new[] { "ImageId" });
            DropIndex("dbo.Profile", new[] { "GenderId" });
            DropIndex("dbo.Image", new[] { "OwnerId" });
            DropIndex("dbo.Image", new[] { "TypeId" });
            DropIndex("dbo.AccreditationBody", new[] { "OwnerId" });
            DropIndex("dbo.AccreditationBody", new[] { "AddressId" });
            DropIndex("dbo.AccreditationBody", new[] { "ImageId" });
            DropIndex("dbo.AccreditationBody", new[] { "WebsiteId" });
            DropForeignKey("dbo.ProfileWall", "Id", "dbo.Wall");
            DropForeignKey("dbo.GroupWall", "Id", "dbo.Wall");
            DropForeignKey("dbo.ActivityWall", "Id", "dbo.Wall");
            DropForeignKey("dbo.TeacherTrainingCourseAccreditations", "AccreditationBodyId", "dbo.AccreditationBody");
            DropForeignKey("dbo.TeacherTrainingCourseAccreditations", "CourseId", "dbo.TeacherTrainingCourse");
            DropForeignKey("dbo.TeacherTrainingOrganisationStyleLink", "StyleId", "dbo.Style");
            DropForeignKey("dbo.TeacherTrainingOrganisationStyleLink", "OrganisationId", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.SearchRecordVenueServiceLink", "VenueServiceId", "dbo.VenueService");
            DropForeignKey("dbo.SearchRecordVenueServiceLink", "RecordId", "dbo.SearchRecord");
            DropForeignKey("dbo.SearchRecordVenueLink", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.SearchRecordVenueLink", "RecordId", "dbo.SearchRecord");
            DropForeignKey("dbo.SearchRecordTeacherServiceLink", "TeacherServiceId", "dbo.TeacherService");
            DropForeignKey("dbo.SearchRecordTeacherServiceLink", "RecordId", "dbo.SearchRecord");
            DropForeignKey("dbo.SearchRecordTeacherLink", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.SearchRecordTeacherLink", "RecordId", "dbo.SearchRecord");
            DropForeignKey("dbo.SearchRecordStyleLink", "StyleId", "dbo.Style");
            DropForeignKey("dbo.SearchRecordStyleLink", "RecordId", "dbo.SearchRecord");
            DropForeignKey("dbo.ReviewComment", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.ReviewComment", "ReviewId", "dbo.Review");
            DropForeignKey("dbo.ReviewExperienceLink", "TypeId", "dbo.ReviewExperience");
            DropForeignKey("dbo.ReviewExperienceLink", "ReviewId", "dbo.Review");
            DropForeignKey("dbo.PrivateConversationParticipant", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.PrivateConversationParticipant", "ConversationId", "dbo.PrivateConversation");
            DropForeignKey("dbo.ActivityStyle", "StyleId", "dbo.Style");
            DropForeignKey("dbo.ActivityStyle", "ActivityId", "dbo.Activity");
            DropForeignKey("dbo.WallPostComment", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.WallPostComment", "WallId", "dbo.WallPost");
            DropForeignKey("dbo.GroupProfession", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.GroupProfession", "GroupId", "dbo.Group");
            DropForeignKey("dbo.GroupStyle", "StyleId", "dbo.Style");
            DropForeignKey("dbo.GroupStyle", "GroupId", "dbo.Group");
            DropForeignKey("dbo.TeacherServiceLink", "ServiceId", "dbo.TeacherService");
            DropForeignKey("dbo.TeacherServiceLink", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherStyleLink", "StyleId", "dbo.Style");
            DropForeignKey("dbo.TeacherStyleLink", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherWebsite", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.TeacherWebsite", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.VenueAmenityLink", "AmenityId", "dbo.VenueAmenity");
            DropForeignKey("dbo.VenueAmenityLink", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.VenueServiceLink", "ServiceId", "dbo.VenueService");
            DropForeignKey("dbo.VenueServiceLink", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.VenueStyleLink", "StyleId", "dbo.Style");
            DropForeignKey("dbo.VenueStyleLink", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.StyleTraitLink", "TraitId", "dbo.StyleTrait");
            DropForeignKey("dbo.StyleTraitLink", "StyleId", "dbo.Style");
            DropForeignKey("dbo.VenueWebsite", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.VenueWebsite", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.ProfileWebsiteLink", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.ProfileWebsiteLink", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.UserActivationRequest", "UserId", "dbo.User");
            DropForeignKey("dbo.TeacherTrainingCourseVenue", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.TeacherTrainingCourseVenue", "CourseId", "dbo.TeacherTrainingCourse");
            DropForeignKey("dbo.TeacherTrainingCourseTeacher", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.TeacherTrainingCourseTeacher", "CourseId", "dbo.TeacherTrainingCourse");
            DropForeignKey("dbo.TeacherTrainingOrganisation", "OwnerId", "dbo.User");
            DropForeignKey("dbo.TeacherTrainingOrganisation", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.TeacherTrainingOrganisation", "AddressId", "dbo.Address");
            DropForeignKey("dbo.TeacherTrainingOrganisation", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.TeacherTrainingOrganisation", "ImageId", "dbo.Image");
            DropForeignKey("dbo.TeacherTrainingCourse", "StyleId", "dbo.Style");
            DropForeignKey("dbo.TeacherTrainingCourse", "ImageId", "dbo.Image");
            DropForeignKey("dbo.TeacherTrainingCourse", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.TeacherTrainingCourse", "OrganisationId", "dbo.TeacherTrainingOrganisation");
            DropForeignKey("dbo.StyleOrganisation", "OwnerId", "dbo.User");
            DropForeignKey("dbo.StyleOrganisation", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.StyleOrganisation", "ImageId", "dbo.Image");
            DropForeignKey("dbo.StyleOrganisation", "LocationId", "dbo.Location");
            DropForeignKey("dbo.StyleOrganisation", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.StyleOrganisation", "StyleId", "dbo.Style");
            DropForeignKey("dbo.SearchRecord", "ParentId", "dbo.SearchRecord");
            DropForeignKey("dbo.SearchRecord", "ImageId", "dbo.Image");
            DropForeignKey("dbo.SearchRecord", "VenueTypeId", "dbo.VenueType");
            DropForeignKey("dbo.SearchRecord", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.ReviewFeedbackResponse", "UserId", "dbo.User");
            DropForeignKey("dbo.ReviewFeedbackResponse", "ReviewId", "dbo.Review");
            DropForeignKey("dbo.ReviewRatingSubject", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.ReviewRating", "ReviewId", "dbo.Review");
            DropForeignKey("dbo.ReviewRating", "SubjectId", "dbo.ReviewRatingSubject");
            DropForeignKey("dbo.Review", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Review", "SubjectHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Review", "AuthorHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.ReviewExperience", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.Request", "ContextHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Request", "SubjectHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Request", "ActorHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Request", "TypeId", "dbo.RequestType");
            DropForeignKey("dbo.Request", "UserId", "dbo.User");
            DropForeignKey("dbo.PrivateMessageDelivery", "MessageId", "dbo.PrivateMessage");
            DropForeignKey("dbo.PrivateMessageDelivery", "RecipientProfileId", "dbo.Profile");
            DropForeignKey("dbo.PrivateMessage", "SenderProfileId", "dbo.Profile");
            DropForeignKey("dbo.PrivateMessage", "ConversationId", "dbo.PrivateConversation");
            DropForeignKey("dbo.VideoAssociation", "HandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.VideoAssociation", "VideoId", "dbo.Video");
            DropForeignKey("dbo.Video", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Video", "TypeId", "dbo.VideoType");
            DropForeignKey("dbo.Video", "AbilityLevelId", "dbo.AbilityLevel");
            DropForeignKey("dbo.Pose", "CategoryId", "dbo.PoseCategory");
            DropForeignKey("dbo.Pose", "VideoId", "dbo.Video");
            DropForeignKey("dbo.Pose", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Pose", "AbilityLevelId", "dbo.AbilityLevel");
            DropForeignKey("dbo.PasswordResetRequest", "LoginId", "dbo.PasswordLogin");
            DropForeignKey("dbo.PasswordResetAction", "RequestId", "dbo.PasswordResetRequest");
            DropForeignKey("dbo.PasswordLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.NotificationCategoryPreferences", "NotificationCategoryId", "dbo.NotificationCategory");
            DropForeignKey("dbo.NotificationCategoryPreferences", "UserId", "dbo.User");
            DropForeignKey("dbo.Notification", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.Notification", "ContextHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Notification", "SubjectHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Notification", "ActorHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Notification", "TypeId", "dbo.NotificationType");
            DropForeignKey("dbo.NotificationDelivery", "UserId", "dbo.User");
            DropForeignKey("dbo.NotificationDelivery", "Notificationid", "dbo.Notification");
            DropForeignKey("dbo.NotificationType", "CategoryId", "dbo.NotificationCategory");
            DropForeignKey("dbo.NewsSubscription", "SubjectHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.NewsSubscription", "SubscriberProfileId", "dbo.Profile");
            DropForeignKey("dbo.NewsStory", "InverseId", "dbo.NewsStory");
            DropForeignKey("dbo.NewsStory", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.NewsStory", "TargetHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.NewsStory", "ActorHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.NewsStory", "SubjectHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.NewsStory", "TypeId", "dbo.NewsStoryType");
            DropForeignKey("dbo.Invitation", "LoginProviderId", "dbo.FederatedLoginProvider");
            DropForeignKey("dbo.Invitation", "UserId", "dbo.Profile");
            DropForeignKey("dbo.FederatedLogin", "ProviderId", "dbo.FederatedLoginProvider");
            DropForeignKey("dbo.FederatedLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.GalleryEntry", "ImageId", "dbo.Image");
            DropForeignKey("dbo.GalleryEntry", "GalleryId", "dbo.Gallery");
            DropForeignKey("dbo.Gallery", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Gallery", "CoverImageId", "dbo.Image");
            DropForeignKey("dbo.GalleryAssociation", "HandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.GalleryAssociation", "GalleryId", "dbo.Gallery");
            DropForeignKey("dbo.Accreditation", "AccreditationBodyId", "dbo.AccreditationBody");
            DropForeignKey("dbo.NotificationPreferences", "EmailDigestTimescaleId", "dbo.Timescale");
            DropForeignKey("dbo.NotificationPreferences", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ActivityTeacher", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.ActivityTeacher", "ActivityId", "dbo.Activity");
            DropForeignKey("dbo.Comment", "ActorHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Comment", "OwnerId", "dbo.User");
            DropForeignKey("dbo.GroupMember", "UserId", "dbo.User");
            DropForeignKey("dbo.GroupMember", "GroupId", "dbo.Group");
            DropForeignKey("dbo.Group", "WallId", "dbo.GroupWall");
            DropForeignKey("dbo.Group", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Group", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.Group", "ImageId", "dbo.Image");
            DropForeignKey("dbo.WallPost", "WallId", "dbo.Wall");
            DropForeignKey("dbo.WallPost", "ActorHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.WallPost", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Fan", "EntityHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Fan", "UserId", "dbo.User");
            DropForeignKey("dbo.EntityHandle", "ParentId", "dbo.EntityHandle");
            DropForeignKey("dbo.EntityHandle", "OwnerId", "dbo.User");
            DropForeignKey("dbo.EntityHandle", "ImageId", "dbo.Image");
            DropForeignKey("dbo.EntityHandle", "EntityTypeId", "dbo.EntityType");
            DropForeignKey("dbo.TeacherAccreditation", "AccreditationBodyId", "dbo.AccreditationBody");
            DropForeignKey("dbo.TeacherAccreditation", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.Teacher", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Teacher", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.Teacher", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Teacher", "BlogId", "dbo.Website");
            DropForeignKey("dbo.Teacher", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Teacher", "AddressId", "dbo.Address");
            DropForeignKey("dbo.TeacherPlacement", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.TeacherPlacement", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.StyleTrait", "GroupId", "dbo.StyleTraitGroup");
            DropForeignKey("dbo.Style", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Style", "WebsiteId", "dbo.Website");
            DropForeignKey("dbo.Style", "DirectoryImageId", "dbo.Image");
            DropForeignKey("dbo.Style", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Address", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Venue", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Venue", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.Venue", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Venue", "BlogId", "dbo.Website");
            DropForeignKey("dbo.Venue", "AddressId", "dbo.Address");
            DropForeignKey("dbo.Venue", "TypeId", "dbo.VenueType");
            DropForeignKey("dbo.Activity", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Activity", "WallId", "dbo.ActivityWall");
            DropForeignKey("dbo.Activity", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.Activity", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Activity", "RepeatFrequencyId", "dbo.ActivityRepeatFrequency");
            DropForeignKey("dbo.Activity", "AbilityLevelId", "dbo.AbilityLevel");
            DropForeignKey("dbo.Activity", "PromoterHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.Activity", "VenueId", "dbo.Venue");
            DropForeignKey("dbo.Activity", "TypeId", "dbo.ActivityType");
            DropForeignKey("dbo.ActivityAttendee", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.ActivityAttendee", "ActivityId", "dbo.Activity");
            DropForeignKey("dbo.Friendship", "InverseId", "dbo.Friendship");
            DropForeignKey("dbo.Friendship", "FriendId", "dbo.Profile");
            DropForeignKey("dbo.Friendship", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.ProfileAnswer", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.ProfileAnswer", "QuestionId", "dbo.ProfileQuestion");
            DropForeignKey("dbo.Profile", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Profile", "WallId", "dbo.ProfileWall");
            DropForeignKey("dbo.Profile", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Profile", "ProfileBannerImageId", "dbo.Image");
            DropForeignKey("dbo.Profile", "ImageId", "dbo.Image");
            DropForeignKey("dbo.Profile", "GenderId", "dbo.Gender");
            DropForeignKey("dbo.Image", "OwnerId", "dbo.User");
            DropForeignKey("dbo.Image", "TypeId", "dbo.ImageType");
            DropForeignKey("dbo.AccreditationBody", "OwnerId", "dbo.User");
            DropForeignKey("dbo.AccreditationBody", "AddressId", "dbo.Address");
            DropForeignKey("dbo.AccreditationBody", "ImageId", "dbo.Image");
            DropForeignKey("dbo.AccreditationBody", "WebsiteId", "dbo.Website");
            DropTable("dbo.ProfileWall");
            DropTable("dbo.GroupWall");
            DropTable("dbo.ActivityWall");
            DropTable("dbo.TeacherTrainingCourseAccreditations");
            DropTable("dbo.TeacherTrainingOrganisationStyleLink");
            DropTable("dbo.SearchRecordVenueServiceLink");
            DropTable("dbo.SearchRecordVenueLink");
            DropTable("dbo.SearchRecordTeacherServiceLink");
            DropTable("dbo.SearchRecordTeacherLink");
            DropTable("dbo.SearchRecordStyleLink");
            DropTable("dbo.ReviewComment");
            DropTable("dbo.ReviewExperienceLink");
            DropTable("dbo.PrivateConversationParticipant");
            DropTable("dbo.ActivityStyle");
            DropTable("dbo.WallPostComment");
            DropTable("dbo.GroupProfession");
            DropTable("dbo.GroupStyle");
            DropTable("dbo.TeacherServiceLink");
            DropTable("dbo.TeacherStyleLink");
            DropTable("dbo.TeacherWebsite");
            DropTable("dbo.VenueAmenityLink");
            DropTable("dbo.VenueServiceLink");
            DropTable("dbo.VenueStyleLink");
            DropTable("dbo.StyleTraitLink");
            DropTable("dbo.VenueWebsite");
            DropTable("dbo.ProfileWebsiteLink");
            DropTable("dbo.UserActivationRequest");
            DropTable("dbo.TeacherTrainingCourseVenue");
            DropTable("dbo.TeacherTrainingCourseTeacher");
            DropTable("dbo.TeacherTrainingOrganisation");
            DropTable("dbo.TeacherTrainingCourse");
            DropTable("dbo.StyleOrganisation");
            DropTable("dbo.SearchRecord");
            DropTable("dbo.ReviewFeedbackResponse");
            DropTable("dbo.ReviewRatingSubject");
            DropTable("dbo.ReviewRating");
            DropTable("dbo.Review");
            DropTable("dbo.ReviewExperience");
            DropTable("dbo.RequestType");
            DropTable("dbo.Request");
            DropTable("dbo.Quote");
            DropTable("dbo.PrivateMessageDelivery");
            DropTable("dbo.PrivateMessage");
            DropTable("dbo.PrivateConversation");
            DropTable("dbo.VideoAssociation");
            DropTable("dbo.VideoType");
            DropTable("dbo.Video");
            DropTable("dbo.Pose");
            DropTable("dbo.PoseCategory");
            DropTable("dbo.PasswordResetRequest");
            DropTable("dbo.PasswordResetAction");
            DropTable("dbo.PasswordLogin");
            DropTable("dbo.NotificationCategoryPreferences");
            DropTable("dbo.Notification");
            DropTable("dbo.NotificationDelivery");
            DropTable("dbo.NotificationType");
            DropTable("dbo.NotificationCategory");
            DropTable("dbo.NewsSubscription");
            DropTable("dbo.NewsStoryType");
            DropTable("dbo.NewsStory");
            DropTable("dbo.Invitation");
            DropTable("dbo.GlossaryEntry");
            DropTable("dbo.FederatedLoginProvider");
            DropTable("dbo.FederatedLogin");
            DropTable("dbo.GalleryEntry");
            DropTable("dbo.Gallery");
            DropTable("dbo.GalleryAssociation");
            DropTable("dbo.Accreditation");
            DropTable("dbo.Timescale");
            DropTable("dbo.NotificationPreferences");
            DropTable("dbo.Role");
            DropTable("dbo.UserRole");
            DropTable("dbo.ActivityTeacher");
            DropTable("dbo.Comment");
            DropTable("dbo.GroupMember");
            DropTable("dbo.Group");
            DropTable("dbo.WallPost");
            DropTable("dbo.Wall");
            DropTable("dbo.ActivityRepeatFrequency");
            DropTable("dbo.Fan");
            DropTable("dbo.EntityType");
            DropTable("dbo.EntityHandle");
            DropTable("dbo.TeacherAccreditation");
            DropTable("dbo.TeacherService");
            DropTable("dbo.Teacher");
            DropTable("dbo.TeacherPlacement");
            DropTable("dbo.VenueAmenity");
            DropTable("dbo.VenueService");
            DropTable("dbo.StyleTraitGroup");
            DropTable("dbo.StyleTrait");
            DropTable("dbo.Style");
            DropTable("dbo.Country");
            DropTable("dbo.Address");
            DropTable("dbo.VenueType");
            DropTable("dbo.Venue");
            DropTable("dbo.ActivityType");
            DropTable("dbo.Activity");
            DropTable("dbo.ActivityAttendee");
            DropTable("dbo.Friendship");
            DropTable("dbo.Location");
            DropTable("dbo.ProfileQuestion");
            DropTable("dbo.ProfileAnswer");
            DropTable("dbo.Gender");
            DropTable("dbo.Profile");
            DropTable("dbo.User");
            DropTable("dbo.ImageType");
            DropTable("dbo.Image");
            DropTable("dbo.Website");
            DropTable("dbo.AccreditationBody");
            DropTable("dbo.AbilityLevel");
        }
    }
}
