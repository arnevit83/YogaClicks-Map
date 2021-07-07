namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewRatingBedrooms : DbMigration
    {
        public override void Up()
        {
            //Sql(@"update ReviewRatingSubject set Name = 'Bedroom' where Name = 'Room'");
        }
        
        public override void Down()
        {
            Sql(@"update ReviewRatingSubject set Name = 'Room' where Name = 'Bedroom'");
        }
    }
}
