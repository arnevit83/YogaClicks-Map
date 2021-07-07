using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchTeacherTrainingOrganisationsResultModel : SearchResultModel
    {
        public SearchTeacherTrainingOrganisationsResultModel(SearchResult result) : base(result)
        {
            Id = result.ParentEntityId.Value;
            Type = result.ParentEntityTypeName;
            Controller = "TeacherTrainingOrganisations";
            Name = result.ParentName;
            Description = result.ParentDescription;
            City = result.ParentCity;
            Area = result.ParentArea;
            Country = result.ParentCountry;
            Postcode = result.ParentPostcode;
            Image = new ImageModel("TeacherTrainingOrganisation.Image", result.ParentImageGuid, result.ParentImageExtension);
            Rating = result.ParentRating;

            CourseResults = new List<SearchResultModel> { new SearchResultModel(result) };
        }

        public IList<SearchResultModel> CourseResults { get; private set; }

    }
}