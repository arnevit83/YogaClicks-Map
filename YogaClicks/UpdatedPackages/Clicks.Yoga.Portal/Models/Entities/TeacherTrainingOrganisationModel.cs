using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Medic;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherTrainingOrganisationModel : PrincipalEntityModel<TeacherTrainingOrganisation>
    {
        public TeacherTrainingOrganisationModel(TeacherTrainingOrganisation entity) : base(entity) {}

        public string Name { get; set; }

        public string Founder { get; set; }

        public int CourseTotal { get; set; }

        public AddressModel Address { get; set; }

        public WebsiteModel Website { get; set; }

        public ImageModel Image { get; set; }

        public ImageModel ProfileBanner { get; set; }

        public string EmailAddress { get; set; }

        public string Description { get; set; }

        public bool Stubbed { get; set; }

        public IList<AccreditationBodyModel> AccreditationBodies { get; private set; } 

        public IList<StyleModel> Styles { get; set; }

        public IList<ConditionModel> Conditions { get; set; }

        public IList<TeacherTrainingCourseModel> Courses { get; set; }

        public override void Populate(TeacherTrainingOrganisation entity)
        {
            Stubbed = entity.Stubbed;
            Name = entity.Name;
            Founder = entity.Founder;
            Website = new WebsiteModel(entity.Website);
            Image = new ImageModel(entity.Image);
            ProfileBanner = new ImageModel(entity.ProfileBanner);
            Address = new AddressModel(entity.Address);
            EmailAddress = entity.EmailAddress;
            Description = entity.Description;

            if (entity.Courses != null)
            {
                CourseTotal = entity.Courses.Count();
            }

            AccreditationBodies = new List<AccreditationBodyModel>();

            foreach (var body in entity.Courses.SelectMany(c => c.AccreditationBodies).Distinct())
                AccreditationBodies.Add(new AccreditationBodyModel(body));

            Styles = new List<StyleModel>();
            Conditions = new List<ConditionModel>();

            foreach (var style in entity.Styles)
                Styles.Add(new StyleModel(style));

            foreach (var condition in entity.Conditions)
                Conditions.Add(new ConditionModel(condition));
        }
    }
}