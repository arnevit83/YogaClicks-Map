using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Sharing
{
    public class SharingEntityByEmailButtonModel
    {
        public SharingEntityByEmailButtonModel(EntityReference reference, bool wide, bool condition)
        {
            Reference = reference;
            Wide = wide;
            Condition = condition;
        }

        public EntityReference Reference { get; set; }

        public bool Wide { get; set; }

        public bool Condition { get; set; }
    }
}