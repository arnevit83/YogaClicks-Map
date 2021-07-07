namespace Clicks.Yoga.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateMessageActors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PrivateMessage", "SenderProfileId", "dbo.Profile");
            DropForeignKey("dbo.PrivateMessageDelivery", "RecipientProfileId", "dbo.Profile");
            DropForeignKey("dbo.PrivateConversationParticipant", "ProfileId", "dbo.Profile");
            DropIndex("dbo.PrivateMessage", new[] { "SenderProfileId" });
            DropIndex("dbo.PrivateMessageDelivery", new[] { "RecipientProfileId" });
            DropIndex("dbo.PrivateConversationParticipant", new[] { "ProfileId" });
            RenameColumn(table: "dbo.PrivateMessage", name: "SenderProfileId", newName: "SenderHandleId");
            RenameColumn(table: "dbo.PrivateMessageDelivery", name: "RecipientProfileId", newName: "RecipientHandleId");
            RenameColumn(table: "dbo.PrivateConversationParticipant", name: "ProfileId", newName: "HandleId");
            
            Sql(
                @"update Message
                set SenderHandleId = Handle.Id
                from dbo.PrivateMessage as Message
                inner join dbo.EntityType as EntityType on EntityType.Name = 'Profile'
                left join dbo.EntityHandle as Handle on Handle.EntityId = Message.SenderHandleId and Handle.EntityTypeId = EntityType.Id");

            Sql(
                @"update Delivery
                set RecipientHandleId = Handle.Id
                from dbo.PrivateMessageDelivery as Delivery
                inner join dbo.EntityType as EntityType on EntityType.Name = 'Profile'
                left join dbo.EntityHandle as Handle on Handle.EntityId = Delivery.RecipientHandleId and Handle.EntityTypeId = EntityType.Id");

            Sql(
                @"update Participant
                set HandleId = Handle.Id
                from dbo.PrivateConversationParticipant as Participant
                inner join dbo.EntityType as EntityType on EntityType.Name = 'Profile'
                left join dbo.EntityHandle as Handle on Handle.EntityId = Participant.HandleId and Handle.EntityTypeId = EntityType.Id");

            Sql("delete from PrivateConversationParticipant where HandleId is null");
            Sql("delete from PrivateMessageDelivery where RecipientHandleId is null");
            Sql("delete from PrivateMessage where SenderHandleId is null");

            AddForeignKey("dbo.PrivateMessage", "SenderHandleId", "dbo.EntityHandle", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrivateMessageDelivery", "RecipientHandleId", "dbo.EntityHandle", "Id");
            AddForeignKey("dbo.PrivateConversationParticipant", "HandleId", "dbo.EntityHandle", "Id");
            CreateIndex("dbo.PrivateMessage", "SenderHandleId");
            CreateIndex("dbo.PrivateMessageDelivery", "RecipientHandleId");
            CreateIndex("dbo.PrivateConversationParticipant", "HandleId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PrivateConversationParticipant", new[] { "HandleId" });
            DropIndex("dbo.PrivateMessageDelivery", new[] { "RecipientHandleId" });
            DropIndex("dbo.PrivateMessage", new[] { "SenderHandleId" });
            DropForeignKey("dbo.PrivateConversationParticipant", "HandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.PrivateMessageDelivery", "RecipientHandleId", "dbo.EntityHandle");
            DropForeignKey("dbo.PrivateMessage", "SenderHandleId", "dbo.EntityHandle");

            Sql(
                @"update Message
                set SenderHandleId = Handle.EntityId
                from dbo.PrivateMessage as Message
                inner join dbo.EntityHandle as Handle on Message.SenderHandleId = Handle.Id");

            Sql(
                @"update Delivery
                set RecipientHandleId = Handle.EntityId
                from dbo.PrivateMessageDelivery as Delivery
                inner join dbo.EntityHandle as Handle on Delivery.RecipientHandleId = Handle.Id");

            Sql(
                @"update Participant
                set HandleId = Handle.EntityId
                from dbo.PrivateConversationParticipant as Participant
                inner join dbo.EntityHandle as Handle on Participant.HandleId = Handle.Id");

            RenameColumn(table: "dbo.PrivateConversationParticipant", name: "HandleId", newName: "ProfileId");
            RenameColumn(table: "dbo.PrivateMessageDelivery", name: "RecipientHandleId", newName: "RecipientProfileId");
            RenameColumn(table: "dbo.PrivateMessage", name: "SenderHandleId", newName: "SenderProfileId");
            CreateIndex("dbo.PrivateConversationParticipant", "ProfileId");
            CreateIndex("dbo.PrivateMessageDelivery", "RecipientProfileId");
            CreateIndex("dbo.PrivateMessage", "SenderProfileId");
            AddForeignKey("dbo.PrivateConversationParticipant", "ProfileId", "dbo.Profile", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PrivateMessageDelivery", "RecipientProfileId", "dbo.Profile", "Id");
            AddForeignKey("dbo.PrivateMessage", "SenderProfileId", "dbo.Profile", "Id", cascadeDelete: true);
        }
    }
}
