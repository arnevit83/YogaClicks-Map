using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class Notification : Entity
    {
        public Notification()
        {
            Deliveries = new List<NotificationDelivery>();
        }

        public virtual NotificationType Type { get; set; }

        public string Message { get; set; }

        public virtual EntityHandle ActorHandle { get; set; }

        public virtual EntityHandle SubjectHandle { get; set; }

        public virtual EntityHandle ContextHandle { get; set; }

        public int? EntityId { get; set; }

        public virtual EntityType EntityType { get; set; }

        public bool Broadcast { get; set; }

        public virtual ICollection<NotificationDelivery> Deliveries { get; private set; }

        public string GetMessage()
        {
            return string.Format(
                Message ?? Type.Message,
                new FormatEntityHandle(ActorHandle, true),
                new FormatEntityHandle(SubjectHandle, false),
                new FormatEntityHandle(ContextHandle, false),
                new FormatEntityHandle(EntityId, EntityType));
        }

        private class FormatEntityHandle : IFormattable
        {
            public FormatEntityHandle(EntityHandle handle, bool actor)
            {
                Actor = actor;
                Handle = handle;
            }

            public FormatEntityHandle(int? entityId, EntityType entityType)
            {
                if (entityId.HasValue && entityType != null)
                {
                    Handle = new EntityHandle { EntityId = entityId.Value, EntityType = entityType };
                }
            }

            private bool Actor { get; set; }

            private EntityHandle Handle { get; set; }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                if (string.IsNullOrEmpty(format)) format = "N";

                if (Actor)
                {
                    return string.Format("<a class=\"nameLink\" href=\"{0:U}\">{0:" + format + "}</a>", Handle);
                }

                var template = "<a href=\"{0:U}\">{0:" + format + "}</a>";

                return string.Format(template, Handle);
            }
        }
    }
}