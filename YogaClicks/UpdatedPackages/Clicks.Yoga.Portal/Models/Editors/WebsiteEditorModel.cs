using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class WebsiteEditorModel
    {
        private string _url;

        public WebsiteEditorModel()
        {
        }

        public WebsiteEditorModel(Website website) : this()
        {
            if (website == null) return;

            Id = website.Id;
            Url = website.Url;
        }

        public Website PopulateEntity(Website entity, IWebsiteService websiteService)
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                if (entity != null)
                {
                    websiteService.DeleteWebsite(entity);
                    entity = null;
                }
            }
            else
            {
                if (entity == null) entity = new Website();
                entity.Url = Url;
            }

            return entity;
        }

        public int? Id { get; set; }

        public string Url
        {
            get { return _url; }
            set { _url = value == null ? null : WebUtility.NormaliseHttpUrl(value); }
        }
    }
}