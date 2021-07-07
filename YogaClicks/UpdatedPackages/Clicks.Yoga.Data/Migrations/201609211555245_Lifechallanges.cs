namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lifechallanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YmStoryLifeChallengeBucket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YmStoryLifeChallenge",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.YmStoryLifeChallengeBucketLink",
                c => new
                    {
                        YmStoryLifeChallengeBucketId = c.Int(nullable: false),
                        YmStoryLifeChallengeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryLifeChallengeBucketId, t.YmStoryLifeChallengeId })
                .ForeignKey("dbo.YmStoryLifeChallenge", t => t.YmStoryLifeChallengeBucketId, cascadeDelete: true)
                .ForeignKey("dbo.YmStoryLifeChallengeBucket", t => t.YmStoryLifeChallengeId, cascadeDelete: true)
                .Index(t => t.YmStoryLifeChallengeBucketId)
                .Index(t => t.YmStoryLifeChallengeId);
            
            CreateTable(
                "dbo.YmStoryLifeChallengeLink",
                c => new
                    {
                        YmStoryId = c.Int(nullable: false),
                        YmStoryLifeChallengeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.YmStoryId, t.YmStoryLifeChallengeId })
                .ForeignKey("dbo.YmStory", t => t.YmStoryId, cascadeDelete: true)
                .ForeignKey("dbo.YmStoryLifeChallenge", t => t.YmStoryLifeChallengeId, cascadeDelete: true)
                .Index(t => t.YmStoryId)
                .Index(t => t.YmStoryLifeChallengeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.YmStoryLifeChallengeLink", new[] { "YmStoryLifeChallengeId" });
            DropIndex("dbo.YmStoryLifeChallengeLink", new[] { "YmStoryId" });
            DropIndex("dbo.YmStoryLifeChallengeBucketLink", new[] { "YmStoryLifeChallengeId" });
            DropIndex("dbo.YmStoryLifeChallengeBucketLink", new[] { "YmStoryLifeChallengeBucketId" });
            DropForeignKey("dbo.YmStoryLifeChallengeLink", "YmStoryLifeChallengeId", "dbo.YmStoryLifeChallenge");
            DropForeignKey("dbo.YmStoryLifeChallengeLink", "YmStoryId", "dbo.YmStory");
            DropForeignKey("dbo.YmStoryLifeChallengeBucketLink", "YmStoryLifeChallengeId", "dbo.YmStoryLifeChallengeBucket");
            DropForeignKey("dbo.YmStoryLifeChallengeBucketLink", "YmStoryLifeChallengeBucketId", "dbo.YmStoryLifeChallenge");
            DropTable("dbo.YmStoryLifeChallengeLink");
            DropTable("dbo.YmStoryLifeChallengeBucketLink");
            DropTable("dbo.YmStoryLifeChallenge");
            DropTable("dbo.YmStoryLifeChallengeBucket");
        }
    }
}
