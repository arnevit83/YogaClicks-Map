namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FannableQuotesAndLingo : DbMigration
    {
        public override void Up()
        {
            Sql(@"update EntityType set Controller = 'Glossary' where Name = 'GlossaryEntry'");
            Sql(@"update EntityType set Controller = 'Quotes' where Name = 'Quote'");
        }
        
        public override void Down()
        {
            Sql(@"update EntityType set Controller = null where Name = 'GlossaryEntry'");
            Sql(@"update EntityType set Controller = null where Name = 'Quote'");
        }
    }
}
