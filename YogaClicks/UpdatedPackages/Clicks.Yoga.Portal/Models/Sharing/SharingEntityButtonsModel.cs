using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Sharing
{
    public class SharingEntityButtonsModel
    {
        public SharingEntityButtonsModel(EntityReference reference, bool condition)
        {
            Reference = reference;
            Condition = condition;
        }

        public EntityReference Reference { get; set; }

        public bool Condition { get; set; }
    }
}