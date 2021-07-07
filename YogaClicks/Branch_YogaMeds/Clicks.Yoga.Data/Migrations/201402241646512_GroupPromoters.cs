namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupPromoters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Group", "PromoterHandleId", c => c.Int());
            AddForeignKey("dbo.Group", "PromoterHandleId", "dbo.EntityHandle", "Id");
            CreateIndex("dbo.Group", "PromoterHandleId");

            Sql(
                @"update [Group]
                set PromoterHandleId = EntityHandle.Id
                from [Group]
                inner join [User] on [Group].OwnerId = [User].Id
                inner join Profile on [User].Id = Profile.OwnerId
                inner join EntityHandle on Profile.Id = EntityHandle.EntityId
                inner join EntityType on EntityHandle.EntityTypeId = EntityType.Id and EntityType.Name = 'Profile'");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Group", new[] { "PromoterHandleId" });
            DropForeignKey("dbo.Group", "PromoterHandleId", "dbo.EntityHandle");
            DropColumn("dbo.Group", "PromoterHandleId");
        }
    }
}
