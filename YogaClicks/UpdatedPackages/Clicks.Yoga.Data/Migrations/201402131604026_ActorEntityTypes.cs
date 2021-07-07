namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActorEntityTypes : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.EntityType", "IsActor", c => c.Boolean(nullable: false));

            //Sql("update dbo.EntityType set IsActor = 1 where Name in ('Profile', 'Teacher', 'Venue')");
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityType", "IsActor");
        }
    }
}
