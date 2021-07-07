namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TypeOfPersonTraitGroupText : DbMigration
    {
        public override void Up()
        {
            //Sql("update StyleTraitGroup set Name = 'Type of person' where Name = 'Type of profile'");
        }
        
        public override void Down()
        {
            Sql("update StyleTraitGroup set Name = 'Type of profile' where Name = 'Type of person'");
        }
    }
}
