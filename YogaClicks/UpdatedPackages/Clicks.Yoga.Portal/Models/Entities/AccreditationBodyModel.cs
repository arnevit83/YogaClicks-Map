using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class AccreditationBodyModel : PrincipalEntityModel<AccreditationBody>
    {
        public AccreditationBodyModel() { }

        public AccreditationBodyModel(AccreditationBody entity) : base(entity) {}

        public string Name { get; set; }

        public string Abbreviation { get; set; }

        public string Description { get; set; }

        public string Programmes { get; set; }
        
        public DateTime? DateFounded { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public string Purpose { get; set; }

        public int? MembershipCount { get; set; }

        public string Founder { get; set; }

        public string Affiliations { get; set; }

        public string ImageCourtesyOf { get; set; }

        public CountryModel Country { get; set; }

        public WebsiteModel Website { get; set; }

        public ImageModel Image { get; set; }

        public ICollection<Accreditation> Accreditations { get; set; }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(Name); }
        }

        public override void Populate(AccreditationBody entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Abbreviation = entity.Abbreviation;
            Description = entity.Description;
            DateFounded = entity.DateFounded;
            EmailAddress = entity.EmailAddress;
            Telephone = entity.Telephone;
            Purpose = entity.Purpose;
            MembershipCount = entity.MembershipCount;
            Founder = entity.Founder;
            ImageCourtesyOf = entity.ImageCourtesyOf;
            Affiliations = entity.Affiliations;
            Accreditations = entity.Accreditations;
            // DUNCAN BREAK
            //Programmes = entity.Programmes;

            if (entity.Address != null && entity.Address.Country != null) Country = new CountryModel(entity.Address.Country);
            if (entity.Website != null) Website = new WebsiteModel(entity.Website);
            if (entity.Image != null) Image = new ImageModel(entity.Image);
        }

       
    }
}