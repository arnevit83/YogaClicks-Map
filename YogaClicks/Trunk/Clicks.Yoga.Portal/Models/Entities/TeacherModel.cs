using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Medic;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherModel : PrincipalEntityModel<Teacher>
    {
        public TeacherModel() { }

        public TeacherModel(Teacher entity) : base(entity) { }

        public string Name { get; set; }

        public string Philosophy { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public LocationModel Location { get; set; }

        public IList<WebsiteModel> Websites { get; set; } 

        public IList<QualificationModel> Qualifications { get; set; }

        public ImageModel Image { get; set; }

        public ImageModel ProfileBanner { get; set; }

        public IList<StyleModel> Styles { get; private set; }

        public IList<ConditionModel> Conditions { get; private set; }

        public IList<TeacherServiceModel> Services { get; private set; }

        public IList<TeacherPlacementModel> Placements { get; private set; }

        public IList<TeacherAccreditationModel> Accreditations { get; private set; }

        public bool Stubbed { get; set; }

        public override void Populate(Teacher entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Philosophy = entity.Philosophy;
            Telephone = entity.Telephone;
            EmailAddress = entity.EmailAddress;
            Stubbed = entity.Stubbed;

            if (entity.Image != null) Image = new ImageModel(entity.Image);
            if (entity.ProfileBanner != null) ProfileBanner = new ImageModel(entity.ProfileBanner);
            if (entity.Location != null) Location = new LocationModel(entity.Location);

            Websites = new List<WebsiteModel>();
            Qualifications = new List<QualificationModel>();
            Styles = new List<StyleModel>();
            Conditions = new List<ConditionModel>();
            Services = new List<TeacherServiceModel>();
            Placements = new List<TeacherPlacementModel>();
            Accreditations = new List<TeacherAccreditationModel>();

            foreach (var website in entity.Websites)
            {
                Websites.Add(new WebsiteModel(website));
            }

            foreach (var qualification in entity.Qualifications)
            {
                Qualifications.Add(new QualificationModel(qualification));
            }

            foreach (var style in entity.Styles)
            {
                Styles.Add(new StyleModel(style));
            }

            foreach (var condition in entity.Conditions)
            {
                Conditions.Add(new ConditionModel(condition));
            }

            foreach (var service in entity.Services)
            {
                Services.Add(new TeacherServiceModel(service));
            }

            foreach (var placement in entity.Placements.Where(p => p.Venue.Active))
            {
                if (placement.Venue.Stubbed)
                    continue;

                Placements.Add(new TeacherPlacementModel(placement));
            }

            foreach (var accreditation in entity.Accreditations)
            {
                Accreditations.Add(new TeacherAccreditationModel(accreditation));
            }
        }
    }
}