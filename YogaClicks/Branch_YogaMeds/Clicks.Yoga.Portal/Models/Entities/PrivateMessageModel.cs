using System;
using System.Text.RegularExpressions;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class PrivateMessageModel : EntityModel<PrivateMessage>
    {
        public PrivateMessageModel(PrivateMessage entity) : base(entity) {}

        public PrivateMessageModel(PrivateMessage entity, EntityHandle sender, EntityHandle recipient) : this(entity)
        {
            Recipient =
                entity.Sender.EntityId == recipient.EntityId && entity.Sender.EntityType.Id == recipient.EntityType.Id ?
                new EntityHandleModel(sender) :
                new EntityHandleModel(recipient);
        }

        public PrivateConversationModel Conversation { get; private set; }

        public EntityHandleModel Sender { get; private set; }

        public EntityHandleModel Recipient { get; private set; }

        public string Content { get; private set; }

        public string Time
        {
            get
            {
                string date = null;

                if (CreationTime.Date == DateTime.Now.Date)
                {
                    date = "Today";
                }
                else if (CreationTime.Date == DateTime.Now.Date.AddDays(-1))
                {
                    date = "Yesterday";
                }
                else
                {
                    date = CreationTime.ToString("dd MMMM yyyy");
                }

                return string.Format("{0}, {1:t}", date, CreationTime);
            }
        }

        public string ContentPreview(int length)
        {
            var text = Regex.Replace(Content, @"\s+", " ");

            if (text.Length > length)
            {
                text = text.Substring(0, length) + "…";
            }

            return text;
        }

        public override void Populate(PrivateMessage entity)
        {
            Conversation = new PrivateConversationModel(entity.Conversation);
            Sender = new EntityHandleModel(entity.Sender);
            Content = entity.Content;
        }
    }
}