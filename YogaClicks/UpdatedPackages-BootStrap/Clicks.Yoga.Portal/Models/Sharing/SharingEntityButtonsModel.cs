using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Sharing
{
    public class SharingEntityButtonsModel
    {
        public SharingEntityButtonsModel(EntityReference reference)
        {
            Reference = reference;
        }

        public EntityReference Reference { get; set; }
    }
}