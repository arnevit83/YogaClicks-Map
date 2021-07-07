namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conditionsmany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.YmStory", "ConditionId", "dbo.Condition");
            DropIndex("dbo.YmStory", new[] { "ConditionId" });
            CreateTable(
                "dbo.YmStoryConditionLink",
                c => new
                    {
                        YmStoryId = c.Int(nullable: false),
                        ConditionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryId, t.ConditionId })
                .ForeignKey("dbo.YmStory", t => t.YmStoryId, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.ConditionId, cascadeDelete: true)
                .Index(t => t.YmStoryId)
                .Index(t => t.ConditionId);
            
            DropColumn("dbo.YmStory", "ConditionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.YmStory", "ConditionId", c => c.Int());
            DropIndex("dbo.YmStoryConditionLink", new[] { "ConditionId" });
            DropIndex("dbo.YmStoryConditionLink", new[] { "YmStoryId" });
            DropForeignKey("dbo.YmStoryConditionLink", "ConditionId", "dbo.Condition");
            DropForeignKey("dbo.YmStoryConditionLink", "YmStoryId", "dbo.YmStory");
            DropTable("dbo.YmStoryConditionLink");
            CreateIndex("dbo.YmStory", "ConditionId");
            AddForeignKey("dbo.YmStory", "ConditionId", "dbo.Condition", "Id");
        }
    }
}
