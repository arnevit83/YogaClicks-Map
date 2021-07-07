namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendshipRequestsCategoryBooleans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotificationCategory", "IsFriendshipRequests", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotificationCategory", "IsFriendshipRequests");
        }
    }
}
