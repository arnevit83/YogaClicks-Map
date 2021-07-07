namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraAbilityLevels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbilityLevel", "IsOpen", c => c.Boolean(nullable: false));

            Sql("update AbilityLevel set [Index] = 2, Name = 'Level 1' where Name = 'Beginner'");
            Sql("update AbilityLevel set [Index] = 3, Name = 'Level 2' where Name = 'Intermediate'");
            Sql("update AbilityLevel set [Index] = 4, Name = 'Level 3' where Name = 'Advanced'");

            Sql("insert into AbilityLevel ([Index], Name, CreationTime, ModificationTime, IsOpen) values (1, 'Beginner', getdate(), getdate(), 0)");
            Sql("insert into AbilityLevel ([Index], Name, CreationTime, ModificationTime, IsOpen) values (5, 'Open', getdate(), getdate(), 1)");
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbilityLevel", "IsOpen");
        }
    }
}
