using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Groups
{
    [DataContract]
    public class GroupCreateStep2Model
    {
        public GroupCreateStep2Model()
        {
            Professions = new EntityTypeChooserModel();
        }
        
        [DataMember]
        public bool Professional { get; set; }

        [DataMember]
        public EntityTypeChooserModel Professions { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(Group entity, IEntityService entityService)
        {
            entity.Professional = Professional;
            entity.ProfessionsRestricted = Professions.Ids.Count > 0;
            Professions.PopulateCollection(entity.Professions, entityService);
        }

        public GroupCreateStep2Model PopulateMetadata(IProfileService profileService)
        {
            Professions.PopulateMetadata(profileService.GetProfessionalEntityTypes());
            return this;
        }
    }
}