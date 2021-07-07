using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateTeacherModel
    {
        public ProfileCreateTeacherModel()
        {
            Websites = new WebsiteCollectionEditorModel();
        }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Telephone { get; set; }

        public WebsiteCollectionEditorModel Websites { get; set; }

        public void PopulateEntity(Teacher entity, IWebsiteService websiteService)
        {
            entity.Name = Name;
            entity.EmailAddress = EmailAddress;
            entity.Telephone = Telephone;
            entity.Location = new Location();

            Websites.PopulateCollection(entity.Websites, websiteService);
        }
    }
}