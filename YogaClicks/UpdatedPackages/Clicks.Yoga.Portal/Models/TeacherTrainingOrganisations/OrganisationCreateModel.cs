using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationCreateModel
    {
        public OrganisationCreateModel() {}

        public OrganisationCreateModel(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Description { get; set; }

        public WebsiteEditorModel Website { get; set; }

        public void PopulateEntity(TeacherTrainingOrganisation entity, IAccountService accountService, IWebsiteService websiteService)
        {
            entity.Owner = accountService.GetUser(UserId);
            entity.Name = Name;
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.EmailAddress = EmailAddress;
            entity.Description = Description;
        }

        
    }
}