//using System.Collections.Generic;
//using System.Linq;
//using Clicks.Yoga.Domain.Entities;
//using Clicks.Yoga.Domain.Services;

//namespace Clicks.Yoga.Portal.Models.Editors
//{
//    public class SignUpVenueSelectorCreatorCollectionEditorModel
//    {
//        public SignUpVenueSelectorCreatorCollectionEditorModel()
//        {
//            Entities = new List<SignUpVenueSelectorCreatorModel>();
//            Entities.Add(new SignUpVenueSelectorCreatorModel());
//        }

//        public SignUpVenueSelectorCreatorCollectionEditorModel(IEnumerable<Venue> collection)
//        {
//            Entities = new List<SignUpVenueSelectorCreatorModel>();

//            foreach (var venue in collection)
//            {
//                Entities.Add(new SignUpVenueSelectorCreatorModel(venue));
//            }

//            if (Entities.Count == 0) Entities.Add(new SignUpVenueSelectorCreatorModel());
//        }

//        public IList<SignUpVenueSelectorCreatorModel> Entities { get; private set; } 

//        public void PopulateCollection(ICollection<Website> collection, IWebsiteService websiteService)
//        {
//            foreach (var model in Entities)
//            {
//                Website website;

//                if (model.Id.HasValue)
//                {
//                    website = collection.FirstOrDefault(e => e.Id == model.Id);
//                    model.PopulateEntity(website, websiteService);
//                }
//                else
//                {
//                    website = model.PopulateEntity(null, websiteService);
//                    collection.Add(website);
//                }
//            }

//            foreach (var website in collection.ToList())
//            {
//                if (website != null && !website.IsTransient && !Entities.Any(e => e.Id == website.Id))
//                {
//                    collection.Remove(website);
//                    websiteService.DeleteWebsite(website);
//                }
//            }
//        }
//    }
//}