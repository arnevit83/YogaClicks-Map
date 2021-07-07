namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlossaryEntityTypeName : DbMigration
    {
        public override void Up()
        {
            Sql("update EntityType set DisplayName = 'Lingo', DisplayPlural = 'Lingo' where Name = 'GlossaryEntry'");
        }
        
        public override void Down()
        {
            Sql("update EntityType set DisplayName = 'Glossary Entry', DisplayPlural = 'Glossary Entries' where Name = 'GlossaryEntry'");
        }
    }
}
