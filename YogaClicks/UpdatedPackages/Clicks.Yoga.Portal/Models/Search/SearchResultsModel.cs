using System.Collections.Generic;
using System.Globalization;
using Clicks.Yoga.Domain.Entities;
using System.Linq;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchResultsModel
    {
        private bool TeacherPlacementSearchRecord = false;

        public SearchResultsModel(SearchCriteriaModel criteria)
        {
            Criteria = criteria;
            CategorisedResults = new Dictionary<string, List<SearchResultModel>>();
        }

        public SearchResultsModel(SearchCriteriaModel criteria, SearchResponse response)
            : this(criteria)
        {
            var unNeededResults = new List<SearchResult>();

            if (criteria.IncludeAllTeacherPlacementRecords == false)
            {
                var teacherList = response.Results.Where(x => x.EntityTypeName == "Teacher").ToList();
                var teacherPlacementList = response.Results.Where(x => x.EntityTypeName == "TeacherPlacement").ToList();

                unNeededResults = teacherList.Where(teacher => teacher.Stubbed || teacherPlacementList.Any(x => x.ParentEntityId == teacher.EntityId)).ToList();
                var groupedRecord = teacherPlacementList.GroupBy(x => x.ParentEntityId).Select(z => z.Key).ToList();

                foreach (var teacherPlacement in groupedRecord)
                {
                    var placement = teacherPlacement;
                    unNeededResults.AddRange(teacherPlacementList.Where(x => x.ParentEntityId == placement).Skip(1).ToList());
                }
            }
            else
            {
                unNeededResults = response.Results.Where(x => x.Stubbed).ToList();
                
            }
            
            var filteredResponse = new SearchResponse(response.Results.Except(unNeededResults),
                response.TotalCount - unNeededResults.Count(),
                response.HasNextPage,
                response.HasPreviousPage,
                response.ExactMatchSearch );

            // here is where it filters down the results
            Response = new SearchResponseModel(filteredResponse);

            foreach (var result in filteredResponse.Results)
            {
                var model = new SearchResultModel(result);

                if (!CategorisedResults.ContainsKey(model.TypePlural))
                    CategorisedResults.Add(model.TypePlural, new List<SearchResultModel>());

                CategorisedResults[model.TypePlural].Add(model);
            }
        }

        public SearchCriteriaModel Criteria { get; private set; }

        public SearchResponseModel Response { get; private set; }

        public Dictionary<string, List<SearchResultModel>> CategorisedResults { get; private set; }

        public IList<SearchResultModel> Results
        {
            get { return Response.Results; }
        }

        public string PageDescription
        {
            get
            {
                if (Response.TotalCount == 1)
                    return "Result: 1";

                return string.Format("Results: {0}", Response.TotalCount);
            }
        }
    }
}