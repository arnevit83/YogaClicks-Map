namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchRecordLocations : DbMigration
    {
        public override void Up()
        {
            Sql(
                @"update SearchRecord
                set City = Location.Name
                from SearchRecord
                inner join EntityType on SearchRecord.EntityTypeId = EntityType.Id and EntityType.Name = 'Profile'
                inner join Profile on SearchRecord.EntityId = Profile.Id
                inner join Location on Profile.LocationId = Location.Id");
            Sql(
                @"update SearchRecord
                set City = Location.Name
                from SearchRecord
                inner join EntityType on SearchRecord.EntityTypeId = EntityType.Id and EntityType.Name = 'StyleOrganisation'
                inner join StyleOrganisation on SearchRecord.EntityId = StyleOrganisation.Id
                inner join Location on StyleOrganisation.LocationId = Location.Id");
            Sql(
                @"update SearchRecord
                set City = Location.Name
                from SearchRecord
                inner join EntityType on SearchRecord.EntityTypeId = EntityType.Id and EntityType.Name = 'Teacher'
                inner join Teacher on SearchRecord.EntityId = Teacher.Id
                inner join Location on Teacher.LocationId = Location.Id");
        }
        
        public override void Down()
        {
            Sql(
                @"update SearchRecord
                set City = null
                from SearchRecord
                inner join EntityType on SearchRecord.EntityTypeId = EntityType.Id
                where EntityType.Name in ('Profile', 'StyleOrganisation', 'Teacher')");
        }
    }
}
