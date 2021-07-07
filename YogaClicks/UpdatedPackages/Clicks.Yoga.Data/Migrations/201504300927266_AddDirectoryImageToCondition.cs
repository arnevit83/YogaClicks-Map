namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirectoryImageToCondition : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Condition", "DirectoryImage_Id", c => c.Int());
            //AddForeignKey("dbo.Condition", "DirectoryImage_Id", "dbo.Image", "Id");
            //CreateIndex("dbo.Condition", "DirectoryImage_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Condition", new[] { "DirectoryImage_Id" });
            DropForeignKey("dbo.Condition", "DirectoryImage_Id", "dbo.Image");
            DropColumn("dbo.Condition", "DirectoryImage_Id");
        }
    }
}
