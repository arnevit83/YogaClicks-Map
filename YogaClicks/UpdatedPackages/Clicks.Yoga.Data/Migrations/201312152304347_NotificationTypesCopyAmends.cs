namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationTypesCopyAmends : DbMigration
    {
        public override void Up()
        {
            //Sql("update NotificationType set Message = '{0:N} reviewed your {1:t}.' where Tag = 'ReviewCreated'");
            //Sql("update NotificationType set Message = '{0:N} became a fan of your {1:t}.' where Tag = 'Fanned'");
            //Sql("update NotificationType set Message = '{0:N} has accepted your friend request.' where Tag = 'FriendshipAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has confirmed that you teach at {1:N}.' where Tag = 'TeacherPlacementAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has confirmed that they teach at {2:N}.' where Tag = 'VenuePlacementAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has confirmed that they teach at {1:N}.' where Tag = 'TeacherTrainingCourseTeacherAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has confirmed that {1:N} is held at {2:N}.' where Tag = 'TeacherTrainingCourseVenueAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has joined your group {1:N}.' where Tag = 'GroupJoined'");
            //Sql("update NotificationType set Message = '{0:N} has accepted your request to join {1:N}.' where Tag = 'GroupMembershipRequestAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has confirmed that they are attending {1:N}.' where Tag = 'ActivityTeacherAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has said that they are not attending {1:N}.' where Tag = 'ActivityTeacherRejected'");
            //Sql("update NotificationType set Message = '{0:N} has confirmed that {1:N} is being held at {2:N}.' where Tag = 'ActivityVenueAccepted'");
            //Sql("update NotificationType set Message = '{0:N} has said that {1:N} is not being held at {2:N}.' where Tag = 'ActivityVenueRejected'");
            //Sql("update NotificationType set Message = '{0:N} has commented on your {3:t}.' where Tag = 'CommentAdded'");
            //Sql("update NotificationType set Message = '{0:N} recommended {1:N} to you.' where Tag = 'Recommendation'");

            //Sql("update RequestType set Message = '{0:N} sent you a friend request.' where Tag = 'FriendshipRequested'");
            //Sql("update RequestType set Message = '{0:N} has said they teach at {2:N}.' where Tag = 'TeacherPlacementAdded'");
            //Sql("update RequestType set Message = '{0:N} has said you teach at {1:N}.' where Tag = 'VenuePlacementAdded'");
            //Sql("update RequestType set Message = '{0:N} has said you teach on the teacher training course {1:N}.' where Tag = 'TeacherTrainingCourseTeacherAdded'");
            //Sql("update RequestType set Message = '{0:N} has said the teacher training course {1:N} is held at {2:N}.' where Tag = 'TeacherTrainingCourseVenueAdded'");
            //Sql("update RequestType set Message = '{0:N} has invited you to join  the group {1:N}.' where Tag = 'GroupInvitation'");
            //Sql("update RequestType set Message = '{0:N} wants to join your group {1:N}.' where Tag = 'GroupMembershipRequest'");
            //Sql("update RequestType set Message = '{0:N} has invited you to {1:N}.' where Tag = 'ActivityInvitation'");
            //Sql("update RequestType set Message = '{0:N} has said you are doing {1:N}.' where Tag = 'ActivityTeacherAdded'");
            //Sql("update RequestType set Message = '{0:N} has said that {1:N} is being held at {2:N}.' where Tag = 'ActivityVenueAdded'");
        }
        
        public override void Down()
        {
        }
    }
}
