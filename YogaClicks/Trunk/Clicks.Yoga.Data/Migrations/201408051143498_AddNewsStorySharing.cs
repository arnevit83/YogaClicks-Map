namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsStorySharing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profile", "PostSharingPermitted", c => c.Boolean(nullable: false));
            AddColumn("dbo.WallPost", "SharedStoryId", c => c.Int());
            AddColumn("dbo.NewsStory", "ShareOriginalId", c => c.Int());
            AddColumn("dbo.NewsStoryType", "Shareable", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.WallPost", "SharedStoryId", "dbo.NewsStory", "Id");
            AddForeignKey("dbo.NewsStory", "ShareOriginalId", "dbo.NewsStory", "Id");
            CreateIndex("dbo.WallPost", "SharedStoryId");
            CreateIndex("dbo.NewsStory", "ShareOriginalId");

            Sql(@"update dbo.NewsStoryType set Shareable = 1 where Tag in ('ActivityPosted', 'FriendAddedPhotos', 'FriendPosted', 'GroupPosted')");

            Sql(
                @"insert into dbo.NewsStoryType (Tag, Name, Vain, Deduped, Shareable, CreationTime, ModificationTime)
                values ('FriendSharedStory', '', 1, 0, 1, getdate(), getdate())");

            Sql(@"update Profile set PostSharingPermitted = 1");
        }
        
        public override void Down()
        {
            DropIndex("dbo.NewsStory", new[] { "ShareOriginalId" });
            DropIndex("dbo.WallPost", new[] { "SharedStoryId" });
            DropForeignKey("dbo.NewsStory", "ShareOriginalId", "dbo.NewsStory");
            DropForeignKey("dbo.WallPost", "SharedStoryId", "dbo.NewsStory");
            DropColumn("dbo.NewsStoryType", "Shareable");
            DropColumn("dbo.NewsStory", "ShareOriginalId");
            DropColumn("dbo.WallPost", "SharedStoryId");
            DropColumn("dbo.Profile", "PostSharingPermitted");
        }
    }
}
