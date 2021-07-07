namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoGroups : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Group", "AutoJoin", c => c.Boolean(nullable: true));
            //Sql("update [Group] set AutoJoin = 0");
            //AlterColumn("dbo.Group", "AutoJoin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Group", "AutoJoin");
        }
    }
}
