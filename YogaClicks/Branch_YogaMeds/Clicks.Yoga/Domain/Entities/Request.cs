using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class Request : Entity
    {
        public virtual User User { get; set; }

        public virtual RequestType Type { get; set; }

        public virtual EntityHandle ActorHandle { get; set; }

        public virtual EntityHandle SubjectHandle { get; set; }

        public virtual EntityHandle ContextHandle { get; set; }

        public int EntityId { get; set; }

        public bool Completed { get; set; }

        public DateTime? CompletionTime { get; set; }

        public string GetMessage()
        {
            return string.Format(
                Type.Message,
                new FormatEntityHandle(ActorHandle, true),
                new FormatEntityHandle(SubjectHandle, false),
                new FormatEntityHandle(ContextHandle, false));
        }

        private class FormatEntityHandle : IFormattable
        {
            public FormatEntityHandle(EntityHandle handle, bool actor)
            {
                Actor = actor;
                Handle = handle;
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