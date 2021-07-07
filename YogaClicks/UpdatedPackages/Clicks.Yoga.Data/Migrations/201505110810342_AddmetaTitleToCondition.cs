namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddmetaTitleToCondition : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Condition", "MetaTitle", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Condition", "MetaTitle");
        }
    }
}
