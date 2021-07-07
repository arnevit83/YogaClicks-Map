namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMetaDescToCondition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Condition", "MetaDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Condition", "MetaDescription");
        }
    }
}
