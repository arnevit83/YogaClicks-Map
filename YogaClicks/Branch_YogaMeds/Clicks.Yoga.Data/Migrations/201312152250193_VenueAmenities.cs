namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VenueAmenities : DbMigration
    {
        public override void Up()
        {
            Sql("if exists(select * from VenueAmenity where Name = 'Sprung studio floor') update VenueAmenity set IsResidential = 1 where Name = 'Sprung studio floor' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Sprung studio floor', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Studio mirrors') update VenueAmenity set IsResidential = 1 where Name = 'Studio mirrors' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Studio mirrors', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Studio rope wall') update VenueAmenity set IsResidential = 1 where Name = 'Studio rope wall' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Studio rope wall', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Practice room views') update VenueAmenity set IsResidential = 1 where Name = 'Practice room views' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Practice room views', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Communal area') update VenueAmenity set IsResidential = 1 where Name = 'Communal area' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Communal area', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Juice bar') update VenueAmenity set IsResidential = 1 where Name = 'Juice bar' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Juice bar', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Single occupancy bedrooms') update VenueAmenity set IsResidential = 1 where Name = 'Single occupancy bedrooms' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Single occupancy bedrooms', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Private bathrooms') update VenueAmenity set IsResidential = 1 where Name = 'Private bathrooms' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Private bathrooms', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Pool') update VenueAmenity set IsResidential = 1 where Name = 'Pool' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Pool', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Sun loungers') update VenueAmenity set IsResidential = 1 where Name = 'Sun loungers' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Sun loungers', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Close to beach') update VenueAmenity set IsResidential = 1 where Name = 'Close to beach' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Close to beach', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Walking/trekking') update VenueAmenity set IsResidential = 1 where Name = 'Walking/trekking' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Walking/trekking', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Area of outstanding natural beauty') update VenueAmenity set IsResidential = 1 where Name = 'Area of outstanding natural beauty' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Area of outstanding natural beauty', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Disabled access') update VenueAmenity set IsResidential = 1 where Name = 'Disabled access' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Disabled access', getdate(), getdate(), 1)");
            Sql("if exists(select * from VenueAmenity where Name = 'Easy transport links') update VenueAmenity set IsResidential = 1 where Name = 'Easy transport links' else insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Easy transport links', getdate(), getdate(), 1)");

            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Free use of mats', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Sprung studio floor', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Studio mirrors', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Studio rope wall', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Practice room views', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Communal area', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Juice bar', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Food', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Community noticeboard', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Changing room', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Showers', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Hairdryers', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Lockers', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Easy transport links', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('On site parking', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Disabled access', getdate(), getdate(), 0)");
            Sql("insert VenueAmenity (Name, CreationTime, ModificationTime, IsResidential) values ('Post yoga socials', getdate(), getdate(), 0)");

            Sql(@"delete amenity
from VenueAmenity amenity
join VenueAmenityLink link on amenity.Id = link.AmenityId
join Venue venue on venue.Id = link.VenueId
join VenueType vtype on vtype.Id = venue.TypeId
where (amenity.IsResidential = 1 and vtype.IsResidential = 0)
or (amenity.IsResidential = 0 and vtype.IsResidential = 1)
");
        }
        
        public override void Down()
        {
        }
    }
}
