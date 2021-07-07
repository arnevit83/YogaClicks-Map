namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherAddressRemoval : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Teacher", "AddressId", "dbo.Address");
            //DropIndex("dbo.Teacher", new[] { "AddressId" });
            //DropColumn("dbo.Teacher", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teacher", "AddressId", c => c.Int());
            CreateIndex("dbo.Teacher", "AddressId");
            AddForeignKey("dbo.Teacher", "AddressId", "dbo.Address", "Id");
        }
    }
}
