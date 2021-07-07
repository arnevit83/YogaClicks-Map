namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupFAQs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Group", "IsGlobalFaqs", c => c.Boolean(nullable: true));
            Sql("update [Group] set IsGlobalFaqs = 0");
            AlterColumn("dbo.Group", "IsGlobalFaqs", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Group", "IsGlobalFaqs");
        }
    }
}
