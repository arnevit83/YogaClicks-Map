using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Sharing
{
    public class SharingEntityByEmailButtonModel
    {
        public SharingEntityByEmailButtonModel(EntityReference reference, bool wide)
        {
            Reference = reference;
            Wide = wide;
        }

        public EntityReference Reference { get; set; }

        public bool Wide { get; set; }
    }
}