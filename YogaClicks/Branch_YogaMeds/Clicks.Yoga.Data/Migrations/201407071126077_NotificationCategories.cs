namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationCategory", "Tag", c => c.String(nullable: false));
            AddColumn("dbo.NotificationCategory", "IncludedInDigest", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.RequestType", "CategoryId", c => c.Int());
            AddForeignKey("dbo.RequestType", "CategoryId", "dbo.NotificationCategory", "Id");
            CreateIndex("dbo.RequestType", "CategoryId");

            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('Administrative', 'Administrative', 0, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('Requests', 'Requests', 0, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('RequestConfirmations', 'Request confirmations', 0, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('PrivateMessages', 'Private messages', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('Comments', 'New comments', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('FriendRequests', 'Friend requests', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('ActivityInvitations', 'Activity invitations', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('GroupMembers', 'Group members joined', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('ActivityAttendees', 'Activity attendees confirmed', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('Recommendations', 'Recommendations', 0, getdate(), getdate())");

            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('GroupInvitations', 'Group invitations', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('GroupMembershipRequests', 'Group membership requests', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('GroupMembershipRequestAcceptedNotifications', 'Group memberships accepted', 1, getdate(), getdate())");
            Sql("insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime) values ('FriendRequestAcceptedNotifications', 'Friend requests accepted', 1, getdate(), getdate())");

            Sql("update NotificationCategory set Tag = 'Reviews', IncludedInDigest = 1 where Name = 'New reviews of me'");
            Sql("update NotificationCategory set Tag = 'Fans', IncludedInDigest = 1 where Name = 'New fans'");

            Sql(
                @"insert into NotificationType (Tag, Message, Icon, CategoryId, CreationTime, ModificationTime)
                select 'ActivityAttendeeConfirmed', '{0:N} will be attending {1:N}.', 'icon-venue.png', c.Id, getdate(), getdate()
                from dbo.NotificationCategory c
                where c.Tag = 'ActivityAttendees'");

            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'Administrative' where t.Tag = 'Alert'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'Reviews' where t.Tag = 'ReviewCreated'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'Fans' where t.Tag = 'Fanned'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'FriendRequestAcceptedNotifications' where t.Tag = 'FriendshipAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'TeacherPlacementAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'VenuePlacementAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'TeacherTrainingCourseTeacherAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'TeacherTrainingCourseVenueAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'GroupMembers' where t.Tag = 'GroupJoined'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'GroupMembershipRequestAcceptedNotifications' where t.Tag = 'GroupMembershipRequestAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'ActivityTeacherAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'ActivityTeacherRejected'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'ActivityVenueAccepted'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'RequestConfirmations' where t.Tag = 'ActivityVenueRejected'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'Comments' where t.Tag = 'CommentAdded'");
            Sql("update t set CategoryId = c.Id from NotificationType t inner join NotificationCategory c on c.Tag = 'Recommendations' where t.Tag = 'Recommendation'");

            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'FriendRequests' where t.Tag = 'FriendshipRequested'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'TeacherPlacementAdded'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'VenuePlacementAdded'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'TeacherTrainingCourseTeacherAdded'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'TeacherTrainingCourseVenueAdded'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'GroupInvitations' where t.Tag = 'GroupInvitation'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'GroupMembershipRequests' where t.Tag = 'GroupMembershipRequest'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'ActivityInvitation'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'ActivityTeacherAdded'");
            Sql("update t set CategoryId = c.Id from RequestType t inner join NotificationCategory c on c.Tag = 'Requests' where t.Tag = 'ActivityVenueAdded'");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestType", new[] { "CategoryId" });
            DropForeignKey("dbo.RequestType", "CategoryId", "dbo.NotificationCategory");
            DropColumn("dbo.RequestType", "CategoryId");

            Sql("update NotificationType set CategoryId = null where Tag not in ('ReviewCreated', 'Fanned')");

            Sql("delete from NotificationType where Tag = 'ActivityAttendeeConfirmed'");

            Sql("delete from NotificationCategory where Tag not in ('Reviews', 'Fans')");

            DropColumn("dbo.NotificationCategory", "Tag");
            DropColumn("dbo.NotificationCategory", "IncludedInDigest");
        }
    }
}
