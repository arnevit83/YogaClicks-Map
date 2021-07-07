using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class WebsiteCollectionEditorModel
    {
        public WebsiteCollectionEditorModel()
        {
            Entities = new List<WebsiteEditorModel>();
            Entities.Add(new WebsiteEditorModel());
        }

        public WebsiteCollectionEditorModel(IEnumerable<Website> collection)
        {
            Entities = new List<WebsiteEditorModel>();

            foreach (var website in collection)
            {
                Entities.Add(new WebsiteEditorModel(website));
            }

            if (Entities.Count == 0) Entities.Add(new WebsiteEditorModel());
        }

        public IList<WebsiteEditorModel> Entities { get; private set; } 

        public void PopulateCollection(ICollection<Website> collection, IWebsiteService websiteService)
        {
            foreach (var model in Entities)
            {
                Website website;

                if (model.Id.HasValue)
                {
                    website = collection.FirstOrDefault(e => e.Id == model.Id);
                    model.PopulateEntity(website, websiteService);
                }
                else
                {
                    website = model.PopulateEntity(null, websiteService);
                    collection.Add(website);
                }
            }

            foreach (var website in collection.ToList())
            {
                if (website != null && !website.IsTransient && !Entities.Any(e => e.Id == website.Id))
                {
                    collection.Remove(website);
                    websiteService.DeleteWebsite(website);
                }
            }
        }
    }
}