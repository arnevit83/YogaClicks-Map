namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShareNotification : DbMigration
    {
        public override void Up()
        {
            Sql(@"insert into NotificationCategory (Tag, Name, IncludedInDigest, CreationTime, ModificationTime)
                values ('Shares', 'Shares', 1, getdate(), getdate())");

            Sql(@"insert into NotificationType (Tag, Message, Icon, CategoryId, CreationTime, ModificationTime)
                select 'StoryShared', '{0:N} shared your post.', 'icon-venue.png', c.Id, getdate(), getdate()
                from NotificationCategory c
                where c.Tag = 'Shares'");
        }
        
        public override void Down()
        {
            Sql(@"delete from NotificationCategory where Tag = 'Shares'");
            Sql(@"delete from NotificationType where Tag = 'StoryShared'");
        }
    }
}
