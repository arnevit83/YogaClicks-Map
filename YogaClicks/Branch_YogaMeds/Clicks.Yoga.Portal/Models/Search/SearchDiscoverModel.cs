using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchDiscoverModel
    {
        public SearchDiscoverModel(
            IEnumerable<AccreditationBody> bodies,
            IEnumerable<TeacherTrainingCourse> courses,
            IEnumerable<Teacher> teachers,
            IEnumerable<Venue> venues,
            IEnumerable<StyleOrganisation> styleOrgs,
            IEnumerable<Activity> activities)
        {
            CategorisedResults = new Dictionary<string, List<SearchResultModel>>();

            CategorisedResults.Add("Venue",
                                   venues.Select(v => new SearchResultModel(new SearchResult {
                                       EntityTypeName = "Venue",
                                       EntityId = v.Id,
                                       Controller = "Venues",
                                       Name = v.Name,
                                       City = v.Address != null ? v.Address.City : "",
                                       Area = v.Address != null ? v.Address.Area : "",
                                       Country = v.Address != null && v.Address.Country != null ? v.Address.Country.EnglishName : "",
                                       Description = v.Description,
                                       ImageExtension = v.Image != null ? v.Image.Type.Extension : null,
                                       ImageGuid = v.Image != null ? (Guid?)v.Image.Guid : null,
                                       ImagePath = "Venue.Image"
                                   })).ToList());

            CategorisedResults.Add("Teacher",
                                   teachers.Select(t => new SearchResultModel(new SearchResult
                                   {
                                       EntityTypeName = "Teacher",
                                       EntityId = t.Id,
                                       Controller = "Teachers",
                                       Name = t.Name,
                                       City = t.Location != null ? t.Location.Name : "",
                                       Area = "",
                                       Country = "",
                                       Description = t.Philosophy,
                                       ImageExtension = t.Image != null ? t.Image.Type.Extension : null,
                                       ImageGuid = t.Image != null ? (Guid?)t.Image.Guid : null,
                                       ImagePath = "Teacher.Image"
                                   })).ToList());

            CategorisedResults.Add("TeacherTrainingCourse",
                                   courses.Select(t => new SearchResultModel(new SearchResult
                                   {
                                       EntityTypeName = "TeacherTrainingCourse",
                                       EntityId = t.Id,
                                       Controller = "TeacherTrainingCourses",
                                       Name = t.Name,
                                       City = t.Organisation != null && t.Organisation.Address != null ? t.Organisation.Address.City : "",
                                       Area = t.Organisation != null && t.Organisation.Address != null ? t.Organisation.Address.Area : "",
                                       Country = t.Organisation != null && t.Organisation.Address != null && t.Organisation.Address.Country != null ? t.Organisation.Address.Country.EnglishName : "",
                                       Description = t.Organisation != null ? t.Organisation.Name : "",
                                       ImageExtension = t.Image != null ? t.Image.Type.Extension : null,
                                       ImageGuid = t.Image != null ? (Guid?)t.Image.Guid : null,
                                       ImagePath = "TeacherTrainingCourse.Image"
                                   })).ToList());

            CategorisedResults.Add("AccreditationBody",
                                   bodies.Select(b => new SearchResultModel(new SearchResult
                                   {
                                       EntityTypeName = "AccreditationBody",
                                       EntityId = b.Id,
                                       Controller = "AccreditationBodys",
                                       Name = b.Name,
                                       Country = b.Address != null && b.Address.Country != null ? b.Address.Country.EnglishName : "",
                                       Description = b.Description ?? "",
                                       ImageExtension = b.Image != null ? b.Image.Type.Extension : null,
                                       ImageGuid = b.Image != null ? (Guid?)b.Image.Guid : null,
                                       ImagePath = "AccreditationBody.Image"
                                   })).ToList());

            CategorisedResults.Add("StyleOrganisation",
                                   styleOrgs.Select(s => new SearchResultModel(new SearchResult
                                   {
                                       EntityTypeName = s.GetEntityTypeName(),
                                       EntityId = s.Id,
                                       Controller = s.GetEntityTypeName() + "s",
                                       Name = s.Name,
                                       Country = "",
                                       Description = s.Description ?? "",
                                       ImageExtension = s.Image != null ? s.Image.Type.Extension : null,
                                       ImageGuid = s.Image != null ? (Guid?)s.Image.Guid : null,
                                       ImagePath = s.GetEntityTypeName() + ".Image"
                                   })).ToList());

            CategorisedResults.Add("Activity",
                                   activities.Select(a => new SearchResultModel(new SearchResult
                                   {
                                       EntityTypeName = a.GetEntityTypeName(),
                                       EntityId = a.Id,
                                       Controller = "Activities",
                                       Name = a.Name,
                                       Country = "",
                                       Description = a.Description ?? "",
                                       ImageExtension = a.Image != null ? a.Image.Type.Extension : null,
                                       ImageGuid = a.Image != null ? (Guid?)a.Image.Guid : null,
                                       ImagePath = a.GetEntityTypeName() + ".Image"
                                   })).ToList());
        }

        public Dictionary<string, List<SearchResultModel>> CategorisedResults { get; private set; }
    }
}