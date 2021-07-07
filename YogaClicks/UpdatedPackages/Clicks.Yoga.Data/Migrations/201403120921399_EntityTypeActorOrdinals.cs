namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityTypeActorOrdinals : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.EntityType", "ActorOrdinal", c => c.Byte());

            //Sql("update dbo.EntityType set ActorOrdinal = 1 where Name = 'Profile'");
            //Sql("update dbo.EntityType set ActorOrdinal = 2 where Name = 'Teacher'");
            //Sql("update dbo.EntityType set ActorOrdinal = 3 where Name = 'Venue'");
            //Sql("update dbo.EntityType set ActorOrdinal = 4 where Name = 'StyleOrganisation'");
            //Sql("update dbo.EntityType set ActorOrdinal = 5 where Name = 'TeacherTrainingOrganisation'");
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityType", "ActorOrdinal");
        }
    }
}
