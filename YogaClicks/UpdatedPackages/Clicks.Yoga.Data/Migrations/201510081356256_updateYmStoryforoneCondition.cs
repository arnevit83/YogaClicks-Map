namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateYmStoryforoneCondition : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.YmStoryConditionLink", "YmStoryId", "dbo.YmStory");
            //DropForeignKey("dbo.YmStoryConditionLink", "ConditionId", "dbo.Condition");
            //DropIndex("dbo.YmStoryConditionLink", new[] { "YmStoryId" });
            //DropIndex("dbo.YmStoryConditionLink", new[] { "ConditionId" });
            AddColumn("dbo.YmStory", "ConditionId", c => c.Int());
            AlterColumn("dbo.YmStory", "Location", c => c.String());
            AddForeignKey("dbo.YmStory", "ConditionId", "dbo.Condition", "Id");
            CreateIndex("dbo.YmStory", "ConditionId");
            DropTable("dbo.YmStoryConditionLink");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.YmStoryConditionLink",
                c => new
                    {
                        YmStoryId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryId, t.ConditionId });
            
            DropIndex("dbo.YmStory", new[] { "ConditionId" });
            DropForeignKey("dbo.YmStory", "ConditionId", "dbo.Condition");
            AlterColumn("dbo.YmStory", "Location", c => c.String(maxLength: 100));
            DropColumn("dbo.YmStory", "ConditionId");
            //CreateIndex("dbo.YmStoryConditionLink", "ConditionId");
            //CreateIndex("dbo.YmStoryConditionLink", "YmStoryId");
            //AddForeignKey("dbo.YmStoryConditionLink", "ConditionId", "dbo.Condition", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.YmStoryConditionLink", "YmStoryId", "dbo.YmStory", "Id", cascadeDelete: true);
        }
    }
}
