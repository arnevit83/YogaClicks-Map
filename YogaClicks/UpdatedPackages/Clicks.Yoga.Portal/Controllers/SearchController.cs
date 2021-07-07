using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Search;
using Microsoft.Data.OData;
using Clicks.Yoga.Portal.Models.Community;
using System;
using Clicks.Yoga.Geography;
using System.Configuration;
using System.Text.RegularExpressions;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Controllers
{
    public class SearchController : YogaController
    {
        public SearchController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IEntityService entityService,
            ISearchService searchService,
            IAccreditationBodyService accreditationBodyService,
            IPoseService poseService,
            IStyleService styleService,
            ITeacherTrainingService teacherTrainingService,
            ITeacherService teachers,
            IVenueService venues,
            IProfileService profileService,
            IActivityService activityService,
            IStyleOrganisationService styleOrganisationService,
             IGroupService groupService,
            IMedicService medicService
            )
            : base(workUnit, securityContext)
        {
            EntityService = entityService;
            SearchService = searchService;
            TeacherService = teachers;
            VenueService = venues;
            AccreditationBodyService = accreditationBodyService;
            PoseService = poseService;
            StyleService = styleService;
            TeacherTrainingService = teacherTrainingService;
            ProfileService = profileService;
            ActivityService = activityService;
            StyleOrganisationService = styleOrganisationService;
            GroupService = groupService;
            MedicService = medicService;
        }

        private IStyleOrganisationService StyleOrganisationService { get; set; }

        private IActivityService ActivityService { get; set; }

        private IEntityService EntityService { get; set; }

        private ISearchService SearchService { get; set; }

        private IAccreditationBodyService AccreditationBodyService { get; set; }

        private IPoseService PoseService { get; set; }

        private IStyleService StyleService { get; set; }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private IVenueService VenueService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IGroupService GroupService { get; set; }

        private IMedicService MedicService { get; set; }

        public ActionResult AutocompleteTeachers(string name, bool? owned)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 3) return Json(new List<object>());

            var type = EntityService.GetEntityType("Teacher");

            var criteria = new SearchCriteria
            {
                EntityTypeIds = new[] { type.Id },
                Keywords = name,
                Owned = owned,
                Stubbed = null
            };

            SearchService.PrepareCriteria(criteria);

            var results = SearchService.Search(criteria).Results;
            var bits = results.Select(r => new { Id = r.EntityId, r.Name });

            return Json(bits, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AutocompleteVenues(string name, bool? owned)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 3) return Json(new List<object>());

            var type = EntityService.GetEntityType("Venue");

            var criteria = new SearchCriteria
            {
                EntityTypeIds = new[] { type.Id },
                Keywords = name,
                Owned = owned,
                Stubbed = null
            };

            SearchService.PrepareCriteria(criteria);

            var results = SearchService.Search(criteria).Results;
            var bits = results.Select(r => new { Id = r.EntityId, r.Name });

            return Json(bits, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(SearchCriteriaModel criteria)
        {
            return RedirectToAction("Discover");
        }

        public ActionResult Results(SearchCriteriaModel model)
        {
            PopulateDefaultLocation(model);

            model.PageIndex = 0;
            model.PageSize = 1000;

            var results = new SearchResultsModel(model, Search(model));

            return View(results);
        }

        public ActionResult Discover()
        {
            return View(new SearchDiscoverModel(
                            AccreditationBodyService.LuckyDip(),
                            TeacherTrainingService.LuckyDip(),
                            TeacherService.LucyDip(),
                            VenueService.LuckyDip(),
                            StyleOrganisationService.LuckyDip(),
                            ActivityService.LuckyDip()));
        }

        public ActionResult Profiles(SearchCriteriaModel model)
        {
            PopulateDefaultLocation(model);

            model.Types = "Profile";
            return View(new SearchProfilesModel(model, Search(model)));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ProfilesNavigation()
        {
            return View();
        }

        public ActionResult Teachers(SearchCriteriaModel model)
        {
            PopulateDefaultLocation(model);

            model.Types = "Teacher";
            var searchResults = Search(model);
            //foreach (var result in searchResults.Results)
            //{
            //    if (model.YogaTherapists)
            //    {
            //        if (!result.IsAccreditedTherapist)
            //        {
            //            searchResults.Results.ro
            //        }
                    
            //    }


            //}


            return View(new SearchTeachersModel(model,searchResults));
        }

        public ActionResult TeachersMap(SearchCriteriaModel model)
        {
            //DW - Commented out for DYC-44 to avoid entering default
            //PopulateDefaultLocation(model);

            model.Types = "Teacher";
            model.PageSize = null;
            SetSearchCriteria(model);
            model.Stubbed = false;
            model.IncludeAllTeacherPlacementRecords = true;
            return View(new SearchTeachersModel(model));
        }

        [HttpPost]
        public ActionResult TeachersJson(SearchCriteriaModel model)
        {
            model.Types = "Teacher";
            model.PageSize = 1000;
            SetSearchCriteria(model);
            //var searchDistance = decimal.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.Search.Distance"]);
            var results = new List<SearchResultModel>();

            foreach (var result in Search(model).Results)
            {
                //var inRadius = InRadius(model.Longitude.Value, model.Latitude.Value, result.LocationGeography.Longitude.Value, result.LocationGeography.Latitude.Value, searchDistance);
                if (model.Location != null)
                {
                    char[] delimiters = new[] { ',', ' ' };  // List of your delimiters

                    List<String> resultArray = new List<string>();

                    if (result.City != null)
                    {
                        resultArray = result.City.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }

                    if (result.Country != null)
                    {
                        foreach (var countryItem in result.Country.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList())
                        {
                            resultArray.Add(countryItem);
                        }
                    }

                    var searchArray = model.Location.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

                    searchArray = CheckCommonAbbr(searchArray, model.Location.ToLower());

                    var matches = resultArray.Intersect(searchArray);

                    if (matches.Any())
                    {
                        results.Add(new SearchResultModel(result));
                    }
                }
                else
                {
                    results.Add(new SearchResultModel(result));
                }

            }

            bool empty = results.Count == 0;

            return Json(new { results, empty });
        }

        private List<string> CheckCommonAbbr(List<string> searchArray, string location)
        {
            if (location.Contains("united kingdom"))
            {
                searchArray.Add("uk");
            }
            if (location.Contains("uk"))
            {
                searchArray.Add("united");
                searchArray.Add("kingdom");
            }
            if (location.Contains("united states"))
            {
                searchArray.Add("usa");
            }
            if (location.Contains("usa"))
            {
                searchArray.Add("united");
                searchArray.Add("states");
            }

            return searchArray;
        }

        public bool InRadius(double criteria_x, double criteria_y, double result_x, double result_y, decimal searchDistance)
        {
            var distance = DistanceAlgorithm.DistanceBetweenPlaces(criteria_x, criteria_y, result_x, result_y);
            return distance <= double.Parse(searchDistance.ToString());
        }

        [HttpPost]
        public ActionResult TeacherInformation(int entityId)
        {
            var venue = TeacherService.GetTeacher(entityId);
            return PartialView(new SearchTeacherInformationModel(venue));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult TeachersNavigation(string action = "Teachers")
        {
            var criteria = GetSearchCriteria();
            var model = new SearchTeachersNavigationModel(action, criteria, TeacherService.GetTeacherServices(), StyleService.GetStyles(), MedicService.GetConditions(), AccreditationBodyService.GetAccreditationBodies());
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult GroupsNavigation(string keywords)
        {
            var groups = SecurityContext.Authenticated ? GroupService.GetJoinedGroups(SecurityContext.CurrentUser.Id) : new List<Clicks.Yoga.Domain.Entities.Group>();
            return PartialView(new CommunityNavigationModel(groups, keywords));
        }

        public ActionResult Venues(SearchCriteriaModel model)
        {
            PopulateDefaultLocation(model);

            model.Types = "Venue";

            return View(new SearchVenuesModel(model, Search(model)));
        }

        public ActionResult VenuesMap(SearchCriteriaModel model)
        {
            //DW - Commented out for DYC-44 to avoid entering default
            //PopulateDefaultLocation(model);

            model.Types = "Venue";
            model.PageSize = null;
            SetSearchCriteria(model);
            return View(new SearchVenuesModel(model));
        }

        [HttpPost]
        public ActionResult VenuesJson(SearchCriteriaModel model)
        {
            model.Types = "Venue";
            model.PageSize = 1000;
            //var distance = decimal.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.Search.Distance"]);
            SetSearchCriteria(model);

            var results = new List<SearchResultModel>();

            foreach (var result in Search(model).Results)
            {
                //var inRadius = InRadius(model.Longitude.Value, model.Latitude.Value, result.LocationGeography.Longitude.Value, result.LocationGeography.Latitude.Value, distance);
                if (model.Location != null)
                {
                    char[] delimiters = new[] { ',', ' ' };  // List of your delimiters
                    var resultArray = result.City.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

                    if (result.Country != null)
                    {
                        foreach (var countryItem in result.Country.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList())
                        {
                            resultArray.Add(countryItem);
                        }
                    }

                    var searchArray = model.Location.ToLower().Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

                    searchArray = CheckCommonAbbr(searchArray, model.Location.ToLower());

                    var matches = resultArray.Intersect(searchArray);

                    if (matches.Any())
                    {
                        results.Add(new SearchResultModel(result));
                    }
                }
                else
                {
                    results.Add(new SearchResultModel(result));
                }
            }

            bool empty = results.Count == 0;

            return Json(new { results, empty });
        }

        [HttpPost]
        public ActionResult VenueInformation(int entityId)
        {
            var venue = VenueService.GetVenue(entityId);
            return PartialView(new SearchVenueInformationModel(venue));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult VenuesNavigation(string action = "Venues")
        {
            var criteria = GetSearchCriteria();
            var model = new SearchVenuesNavigationModel(action, criteria, VenueService.GetVenueServices(), VenueService.GetVenueTypes(), StyleService.GetStyles(), MedicService.GetConditions());
            //model.Criteria.Location = null;
            return View(model);
        }

        public ActionResult TeacherTrainingOrganisations(SearchCriteriaModel model)
        {
            model.Types = "TeacherTrainingOrganisation";
            return View(new SearchTeacherTrainingOrganisationsModel(model, Search(model)));
        }

        public ActionResult TeacherTrainingCourses(SearchCriteriaModel model)
        {
            PopulateDefaultLocation(model);

            model.Types = "TeacherTrainingCourse";
            var searchResults = Search(model);
            var parentTTOIds = searchResults.Results.Select(x => x.ParentEntityId.Value).Distinct().ToList();

            var parentTTOs = TeacherTrainingService.GetOrganisationsById(parentTTOIds);

            foreach (var result in searchResults.Results)
            {
                var thisTTO = parentTTOs.Where(x => x.Id == result.ParentEntityId).FirstOrDefault();

                if (thisTTO != null)
                {
                    if (thisTTO.Image != null)
                    {
                        if (thisTTO.Image.Guid != null)
                        {
                            result.ParentImageGuid = thisTTO.Image.Guid;
                            result.ParentImagePath = thisTTO.Image.Path ?? null;
                            result.ParentImageExtension = thisTTO.Image.Type.Extension ?? null;
                        }
                    }
                }
            }

            return View(new SearchTeacherTrainingCoursesModel(model, searchResults));
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult TeacherTrainingNavigation(bool organisations = true)
        {
            var criteria = GetSearchCriteria();
            var model = new SearchTeacherTrainingNavigationModel(criteria, StyleService.GetStyles(), MedicService.GetConditions(), AccreditationBodyService.GetAccreditationBodies());
            model.Organisations = organisations;
            return View(model);
        }

        public ActionResult StyleOrganisations(SearchCriteriaModel model)
        {
            model.Types = "StyleOrganisation";
            return View(new SearchResultsModel(model, Search(model)));
        }

        public ActionResult AccreditingBodies(SearchCriteriaModel model)
        {
            model.Types = "AccreditationBody";
            return View(new SearchAccreditingBodiesModel(model, Search(model)));
        }

        public ActionResult Groups(SearchCriteriaModel model)
        {
            model.Types = "Group";
            return View(new SearchGroupsModel(model, Search(model)));
        }

        public ActionResult ReIndexPose(int id)
        {
            var pose = PoseService.GetPose(id);

            SearchService.IndexElastic(pose);

            return Content("OK");
        }

        public ActionResult ReIndexStyle(int id)
        {
            var style = StyleService.GetStyle(id);

            SearchService.IndexElastic(style);

            return Content("OK");
        }

        public ActionResult ReIndexProfile(int id)
        {
            var profile = ProfileService.GetProfile(id);

            SearchService.IndexElastic(profile);

            return Content("OK");
        }

        public ActionResult ReIndexVenue(int id)
        {
            var venue = VenueService.GetVenue(id);

            SearchService.IndexElastic(venue);

            return Content("OK");
        }

        public ActionResult ReIndexTeacher(int id)
        {
            var teacher = TeacherService.GetTeacher(id);

            SearchService.IndexElastic(teacher);

            return Content("OK");
        }

        public ActionResult ReIndexStyleOrgs()
        {
            var orgs = StyleOrganisationService.GetOrganisations();

            foreach (var org in orgs)
            {
                SearchService.IndexElastic(org);
            }

            return Content("OK");
        }

        private SearchResponse Search(SearchCriteriaModel model)
        {
            var criteria = new SearchCriteria();

            model.PopulateEntity(criteria, EntityService);

            SetSearchCriteria(model);

            if (SecurityContext.Authenticated && SecurityContext.CurrentUser.Profile != null)
            {
                criteria.ProfileId = SecurityContext.CurrentUser.Profile.Id;
            }

            SearchService.PrepareCriteria(criteria);

            return SearchService.Search(criteria);
        }

        private SearchCriteriaModel PopulateDefaultLocation(SearchCriteriaModel model)
        {
            if (!SecurityContext.Authenticated) return model;
            if (SecurityContext.CurrentProfile.Location == null) return model;
            if (model.LocationKnown) return model;

            model.Location = SecurityContext.CurrentProfile.Location.Name;
            model.Latitude = SecurityContext.CurrentProfile.Location.Geography.Latitude;
            model.Longitude = SecurityContext.CurrentProfile.Location.Geography.Longitude;
            model.SortOrder = SearchSortOrder.Distance;

            return model;
        }

        protected SearchCriteriaModel GetSearchCriteria()
        {
            var session = Session["SearchCriteria"] as SearchCriteriaModel;

            if (session == null)
            {
                var model = new SearchCriteriaModel();
                var criteria = new SearchCriteria();

                SearchService.PrepareCriteria(criteria);

                model.SortOrder = criteria.SortOrder;

                return model;
            }

            return session;
        }

        public ActionResult NoResults()
        {
            return View();
        }

        public ActionResult SearchResultPartial(SearchResultModel result)
        {
            if (result.Type == "Condition")
            {
                var condition = MedicService.GetCondition(result.Id);
                
                if (condition != null)
                {
                    if (condition.DirectoryImage != null)
                    {
                        result.Image = new ImageModel(condition.DirectoryImage);
                    }
                }
            }

            return PartialView("SearchResultPartial", result);
        }
    }
}