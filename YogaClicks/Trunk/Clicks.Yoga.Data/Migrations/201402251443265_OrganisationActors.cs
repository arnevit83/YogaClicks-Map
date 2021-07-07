namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisationActors : DbMigration
    {
        public override void Up()
        {
            Sql("update EntityType set IsActor = 1 where Name in ('StyleOrganisation', 'TeacherTrainingOrganisation')");
        }
        
        public override void Down()
        {
            Sql("update EntityType set IsActor = 0 where Name in ('StyleOrganisation', 'TeacherTrainingOrganisation')");
        }
    }
}
