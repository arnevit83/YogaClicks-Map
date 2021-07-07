using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class RequestType : Entity
    {
        public string Tag { get; set; }

        public string Message { get; set; }

        public string Icon { get; set; }

        public string CompletionHandlerTypeName { get; set; }

        public virtual NotificationCategory Category { get; set; }

        public bool IsFriend { get; set; }

        public Type GetCompletionHandlerType()
        {
            return Type.GetType(CompletionHandlerTypeName);
        }
    }
}