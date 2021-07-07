using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class EntityType : Entity, INamed
    {
        public string Name { get; set; }

        public string SystemName { get; set; }

        public string DisplayName { get; set; }

        public string DisplayPlural { get; set; }

        public string Controller { get; set; }

        public bool IsActor { get; set; }

        public bool IsHuman { get; set; }

        public bool IsProfessional { get; set; }

        public bool IsFanable { get; set; }

        public bool IsReviewable { get; set; }

        public bool IsSearchable { get; set; }

        public bool IsGalleryAssociate { get; set; }

        public byte? ActorOrdinal { get; set; }

        public bool CanReview
        {
            get { return IsHuman || GetSystemType() == typeof(Venue); }
        }

        public bool IsTeacher
        {
            get { return GetSystemType() == typeof(Teacher); }
        }

        public Type GetSystemType()
        {
            return Type.GetType(SystemName);
        }
    }
}