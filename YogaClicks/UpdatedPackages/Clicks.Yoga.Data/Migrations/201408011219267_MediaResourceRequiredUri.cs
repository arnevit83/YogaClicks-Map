namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaResourceRequiredUri : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.MediaResource", "Uri", c => c.String(nullable: false, maxLength: 1000));
            //CreateIndex("dbo.MediaResource", "Uri");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MediaResource", new[] { "Uri" });
            AlterColumn("dbo.MediaResource", "Uri", c => c.String());
        }
    }
}
