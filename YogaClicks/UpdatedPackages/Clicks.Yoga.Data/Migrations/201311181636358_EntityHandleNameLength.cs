namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityHandleNameLength : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.EntityHandle", "Name", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EntityHandle", "Name", c => c.String(maxLength: 100));
        }
    }
}
