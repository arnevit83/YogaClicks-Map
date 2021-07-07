namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWTSSAsCollection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WhatTheScienceSays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WhatTheScienceSaysTherapyType",
                c => new
                    {
                        WhatTheScienceSays_Id = c.Int(nullable: false),
                        TherapyType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WhatTheScienceSays_Id, t.TherapyType_Id })
                .ForeignKey("dbo.WhatTheScienceSays", t => t.WhatTheScienceSays_Id, cascadeDelete: true)
                .ForeignKey("dbo.TherapyType", t => t.TherapyType_Id, cascadeDelete: true)
                .Index(t => t.WhatTheScienceSays_Id)
                .Index(t => t.TherapyType_Id);
            
            CreateTable(
                "dbo.WhatTheScienceSaysCondition",
                c => new
                    {
                        WhatTheScienceSays_Id = c.Int(nullable: false),
                        Condition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WhatTheScienceSays_Id, t.Condition_Id })
                .ForeignKey("dbo.WhatTheScienceSays", t => t.WhatTheScienceSays_Id, cascadeDelete: true)
                .ForeignKey("dbo.Condition", t => t.Condition_Id, cascadeDelete: true)
                .Index(t => t.WhatTheScienceSays_Id)
                .Index(t => t.Condition_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.WhatTheScienceSaysCondition", new[] { "Condition_Id" });
            DropIndex("dbo.WhatTheScienceSaysCondition", new[] { "WhatTheScienceSays_Id" });
            DropIndex("dbo.WhatTheScienceSaysTherapyType", new[] { "TherapyType_Id" });
            DropIndex("dbo.WhatTheScienceSaysTherapyType", new[] { "WhatTheScienceSays_Id" });
            DropForeignKey("dbo.WhatTheScienceSaysCondition", "Condition_Id", "dbo.Condition");
            DropForeignKey("dbo.WhatTheScienceSaysCondition", "WhatTheScienceSays_Id", "dbo.WhatTheScienceSays");
            DropForeignKey("dbo.WhatTheScienceSaysTherapyType", "TherapyType_Id", "dbo.TherapyType");
            DropForeignKey("dbo.WhatTheScienceSaysTherapyType", "WhatTheScienceSays_Id", "dbo.WhatTheScienceSays");
            DropTable("dbo.WhatTheScienceSaysCondition");
            DropTable("dbo.WhatTheScienceSaysTherapyType");
            DropTable("dbo.WhatTheScienceSays");
        }
    }
}
