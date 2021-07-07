namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityHandlesActive : DbMigration
    {
        public override void Up()
        {
//            AddColumn("dbo.EntityHandle", "Active", c => c.Boolean(nullable: false));

//            Sql("update EntityHandle set Active = 1");

//            var typeNames = new[] {
//                "AccreditationBody",
//                "Activity",
//                "Group",
//                "Profile",
//                "StyleOrganisation",
//                "Teacher",
//                "TeacherTrainingOrganisation",
//                "Venue"
//            };

//            foreach (var typeName in typeNames)
//            {
//                Sql(string.Format(
//                    @"update h
//                    set Active = e.Active
//                    from EntityHandle h
//                    inner join EntityType t on h.EntityTypeId = t.Id and t.Name = '{0}'
//                    inner join [{0}] e on h.EntityId = e.Id",
//                    typeName));
//            }
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntityHandle", "Active");
        }
    }
}
