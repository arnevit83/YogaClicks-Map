namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityAttendeeHandles : DbMigration
    {
        public override void Up()
        {
//            DropForeignKey("dbo.ActivityAttendee", "ProfileId", "dbo.Profile");
//            DropIndex("dbo.ActivityAttendee", new[] { "ProfileId" });
//            AddColumn("dbo.ActivityAttendee", "HandleId", c => c.Int(nullable: true));
//            AddForeignKey("dbo.ActivityAttendee", "HandleId", "dbo.EntityHandle", "Id");
//            AddColumn("dbo.EntityType", "IsHuman", c => c.Boolean(nullable: false));

//            Sql(
//                @"update ActivityAttendee
//                set HandleId = EntityHandle.Id
//                from ActivityAttendee
//                inner join EntityHandle on ActivityAttendee.ProfileId = EntityHandle.EntityId
//                inner join EntityType on EntityHandle.EntityTypeId = EntityType.Id and EntityType.Name = 'Profile'");

//            Sql(@"update EntityType set IsHuman = 1 where Name in ('Profile', 'Teacher')");

//            AlterColumn("dbo.ActivityAttendee", "HandleId", c => c.Int(nullable: false));
//            CreateIndex("dbo.ActivityAttendee", "HandleId");
//            DropColumn("dbo.ActivityAttendee", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActivityAttendee", "ProfileId", c => c.Int(nullable: true));
            AddForeignKey("dbo.ActivityAttendee", "ProfileId", "dbo.Profile", "Id", cascadeDelete: true);
            DropIndex("dbo.ActivityAttendee", new[] { "HandleId" });
            DropForeignKey("dbo.ActivityAttendee", "HandleId", "dbo.EntityHandle");
            DropColumn("dbo.EntityType", "IsHuman");

            Sql(
                @"update ActivityAttendee
                set ProfileId = EntityHandle.EntityId
                from ActivityAttendee
                inner join EntityHandle on ActivityAttendee.HandleId = EntityHandle.Id");

            AlterColumn("dbo.ActivityAttendee", "ProfileId", c => c.Int(nullable: false));
            CreateIndex("dbo.ActivityAttendee", "ProfileId");
            DropColumn("dbo.ActivityAttendee", "HandleId");
        }
    }
}
