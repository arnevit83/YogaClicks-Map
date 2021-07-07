using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Sharing
{
    public class SharingEntityByEmailModel
    {
        public SharingEntityByEmailModel() {}

        public SharingEntityByEmailModel(EntityReference reference, bool? condition)
        {
            Reference = reference;
            Condition = condition ?? false;
        }

        public EntityReference Reference { get; set; }

        public string EmailAddress { get; set; }

        public string Message { get; set; }

        public bool Condition { get; set; }
    }
}