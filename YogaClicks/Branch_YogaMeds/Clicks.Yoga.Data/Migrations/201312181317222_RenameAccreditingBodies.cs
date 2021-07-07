namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameAccreditingBodies : DbMigration
    {
        public override void Up()
        {
            Sql("update EntityType set DisplayName = 'Accrediting Body', DisplayPlural = 'Accrediting Bodies' where SystemName = 'Clicks.Yoga.Domain.Entities.AccreditationBody'");
        }
        
        public override void Down()
        {
            Sql("update EntityType set DisplayName = 'Accreditation Body', DisplayPlural = 'Accreditation Bodies' where SystemName = 'Clicks.Yoga.Domain.Entities.AccreditationBody'");
        }
    }
}
