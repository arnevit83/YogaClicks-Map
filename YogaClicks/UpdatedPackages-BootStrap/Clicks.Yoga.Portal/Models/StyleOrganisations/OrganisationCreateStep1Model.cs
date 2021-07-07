using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.StyleOrganisations
{
    [DataContract]
    public class OrganisationCreateStep1Model
    {
        public OrganisationCreateStep1Model()
        {
            Style = new StyleSelectorModel();
            FoundedYear = DateTime.Now.Year;
        }

        public OrganisationCreateStep1Model(int ownerId) : this()
        {
            OwnerId = ownerId;
        }

        [DataMember]
        public int OwnerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Founder { get; set; }

        [DataMember]
        public int FoundedYear { get; set; }

        [DataMember]
        public StyleSelectorModel Style { get; private set; }

        public IEnumerable<int> FoundedYearOptions 
        {
            get
            {
                for (int i = 1900; i <= DateTime.Now.Year; i++)
                {
                    yield return i;
                }
            }
        }

        public void PopulateEntity(StyleOrganisation entity, IStyleService styleService)
        {
            entity.Name = Name;
            entity.Founder = Founder;
            entity.FoundedYear = FoundedYear;
            entity.Style = styleService.GetStyle(Style.Id);
        }

        public OrganisationCreateStep1Model PopulateMetadata(IStyleService styleService)
        {
            Style.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}