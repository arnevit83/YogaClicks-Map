using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchTeacherTrainingCoursesModel : SearchResultsModel
    {
        public SearchTeacherTrainingCoursesModel(SearchCriteriaModel criteria, SearchResponse response) : base(criteria, response)
        {
            OrganisationResults = new List<SearchTeacherTrainingOrganisationsResultModel>();

            SearchTeacherTrainingOrganisationsResultModel organisation = null;

            foreach (var result in response.Results)
            {
                if (organisation == null || result.ParentEntityId != organisation.Id)
                {
                    organisation = new SearchTeacherTrainingOrganisationsResultModel(result);
                    OrganisationResults.Add(organisation);
                }
                else
                {
                    organisation.CourseResults.Add(new SearchResultModel(result));
                }
            }
        }

        public IList<SearchTeacherTrainingOrganisationsResultModel> OrganisationResults { get; private set; }
    }
}