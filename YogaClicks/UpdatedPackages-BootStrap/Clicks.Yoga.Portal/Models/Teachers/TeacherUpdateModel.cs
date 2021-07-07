using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherUpdateModel
    {
        public TeacherUpdateModel()
        {
            Location = new LocationEditorModel();
            Websites = new WebsiteCollectionEditorModel();
            Qualifications = new QualificationsCollectionEditorModel();
            Styles = new StyleChooserModel();
            AccreditationBodies = new TeacherAccreditationChooserModel();
            Services = new TeacherServiceChooserModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
        }

        public TeacherUpdateModel(Teacher teacher)
        {
            Name = teacher.Name;
            Location = new LocationEditorModel(teacher.Location);
            Websites = new WebsiteCollectionEditorModel(teacher.Websites);
            Qualifications = new QualificationsCollectionEditorModel(teacher.Qualifications);
            Philosophy = teacher.Philosophy;
            Telephone = teacher.Telephone;
            EmailAddress = teacher.EmailAddress;
            Styles = new StyleChooserModel(teacher.Styles);
            AccreditationBodies = new TeacherAccreditationChooserModel(teacher.Accreditations.Select(e => e.AccreditationBody));
            Services = new TeacherServiceChooserModel(teacher.Services);
            Image = new ProfileImageEditorModel(teacher.Image);
            ProfileBanner = new ProfileBannerEditorModel(teacher.ProfileBanner);
            NewsletterOptOut = teacher.NewsletterOptOut;
        }

        public void PopulateEntity(
            Teacher entity,
            IImageService imageService,
            IStyleService styleService,
            ITeacherService teacherService,
            IAccreditationBodyService accreditationBodies,
            IWebsiteService websiteService,
            IQualificationService qualificationService)
        {
            entity.Name = Name;
            entity.Philosophy = Philosophy;
            entity.Telephone = Telephone;
            entity.EmailAddress = EmailAddress;
            entity.Location = Location.PopulateEntity(entity.Location);
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
            entity.NewsletterOptOut = NewsletterOptOut;

            Websites.PopulateCollection(entity.Websites, websiteService);
            Qualifications.PopulateCollection(entity.Qualifications, qualificationService);
            Styles.PopulateCollection(entity.Styles, styleService);
            Services.PopulateCollection(entity.Services, teacherService);
            AccreditationBodies.PopulateCollection(entity.Accreditations, accreditationBodies);
        }

        public TeacherModel Teacher { get; private set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public LocationEditorModel Location { get; set; }

        public WebsiteCollectionEditorModel Websites { get; private set; }

        public QualificationsCollectionEditorModel Qualifications { get; private set; }

        public string Philosophy { get; set; }

        public StyleChooserModel Styles { get; private set; }

        public TeacherAccreditationChooserModel AccreditationBodies { get; private set; }

        public TeacherServiceChooserModel Services { get; private set; }

        public ProfileImageEditorModel Image { get; private set; }

        public bool NewsletterOptOut { get; set; }

        public ProfileBannerEditorModel ProfileBanner { get; private set; }

        public TeacherUpdateModel PopulateMetadata(
            Teacher teacher,
            IStyleService styleService,
            ITeacherService teacherService,
            IAccreditationBodyService accreditationBodyService)
        {
            Teacher = new TeacherModel(teacher);

            Styles.PopulateMetadata(styleService.GetStyles());
            Services.PopulateMetadata(teacherService.GetTeacherServices());
            AccreditationBodies.PopulateMetadata(accreditationBodyService.GetAccreditationBodies());

            return this;
        }
    }
}