namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WallPostCommentKeyNameCorrection : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WallPostComment", name: "WallId", newName: "PostId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.WallPostComment", name: "PostId", newName: "WallId");
        }
    }
}
