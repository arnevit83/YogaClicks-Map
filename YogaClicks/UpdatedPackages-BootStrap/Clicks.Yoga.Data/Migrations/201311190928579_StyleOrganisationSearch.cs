namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StyleOrganisationSearch : DbMigration
    {
        public override void Up()
        {
            Sql(
                @"insert into SearchRecord (EntityId, EntityTypeId, Name, Description, Location, Styles, Stubbed, ImageId, CreationTime, ModificationTime)
                select o.Id, 26, o.Name, o.Description, l.Geography, s.Name, 0, o.ImageId, getdate(), getdate()
                from StyleOrganisation o
                inner join Location l on o.LocationId = l.Id
                inner join Style s on o.StyleId = s.Id
                where not exists (select * from SearchRecord where EntityId = o.Id and EntityTypeId = 26)");
        }
        
        public override void Down()
        {
            Sql("@delete from SearchRecord where EntityTypeId = 26");
        }
    }
}
