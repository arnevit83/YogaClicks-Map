using System;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Search;
using Clicks.Yoga.Portal.Models.Shared;
using Common;
using Microsoft.Web.Mvc;
using System.Globalization;
using System.Collections.Generic;
using System.Web.UI;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Emails;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using Clicks.Yoga.Portal.Models.SignUp;

namespace Clicks.Yoga.Portal.Controllers
{
    public class ActivitiesController : YogaController
    {
        public ActivitiesController(
            IWorkUnit workUnit,
            IEmailService emailService,
            ISecurityContext securityContext,
            IActivityService activityService,
            IAddressService addressService,
            IEntityService entityService,
            IImageService imageService,
            IProfileService profileService,
            IStyleService styleService,
            IMedicService medicService,
            ITeacherService teacherService,
            ITeacherTrainingService teacherTrainingService,
            IVenueService venueService,
            IFanService fanService)
            : base(workUnit, securityContext)
        {
            ActivityService = activityService;
            AddressService = addressService;
            EntityService = entityService;
            ImageService = imageService;
            ProfileService = profileService;
            StyleService = styleService;
            TeacherService = teacherService;
            TeacherTrainingService = teacherTrainingService;
            VenueService = venueService;
            FanService = fanService;
            MedicService = medicService;
            EmailService = emailService;
        }

        private IActivityService ActivityService { get; set; }

        private IAddressService AddressService { get; set; }

        private IEntityService EntityService { get; set; }

        private IImageService ImageService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IStyleService StyleService { get; set; }

        private IMedicService MedicService { get; set; }

        private ITeacherService TeacherService { get; set; }

        private ITeacherTrainingService TeacherTrainingService { get; set; }

        private IVenueService VenueService { get; set; }

        private IFanService FanService { get; set; }

        private IEmailService EmailService { get; set; }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Navigation()
        {
            var criteria = GetActivitySearchCriteria();
            return PartialView(new ActivityNavigationModel(criteria, ActivityService.GetActivityTypes(), StyleService.GetStyles(), MedicService.GetConditions(), ActivityService.GetAbilityLevels()));
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Tabs(int id)
        {
            var permissions = ActivityService.GetPermissions(id);
            return PartialView(new ActivityTabsModel(permissions));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Tabsmini(int id)
        {
            var permissions = ActivityService.GetPermissions(id);
            return PartialView(new ActivityTabsModel(permissions));
        }




        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult AttendanceButton(int id)
        {
            var activity = ActivityService.GetActivity(id);

            bool finished = false;

            if (activity != null)
            {
                if (activity.FinishTime < DateTime.Now)
                {
                    finished = true;
                }
            }

            var status = ActivityAttendanceStatus.Open;

            if (SecurityContext.Authenticated)
            {
                status = ActivityService.GetAttendanceStatus(id, SecurityContext.CurrentActor);
            }

            return View("AttendanceButton", new ActivityAttendanceButtonModel(status, finished));
        }


        [YogaAuthorize]
        public ActionResult AttendanceActorPicker(int id)
        {
            return View(new ActivityAttendanceActorPickerModel(SecurityContext.CurrentActor.Id, SecurityContext.CurrentActor.GetEntityTypeName()));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult AttendanceActorPicker(int id, ActivityAttendanceActorPickerModel model)
        {
            if (model.ActorId == 0 || model.ActorTypeName == null)
            {
                return View(model);
            }

            var handle = SecurityContext.AvailableActors
                .FirstOrDefault(a => a.EntityId == model.ActorId && a.EntityType.Name == model.ActorTypeName);

            if (handle == null)
            {
                throw new PermissionDeniedException();
            }

            var actor = EntityService.GetEntity<IActor>(handle);

            if (handle == null)
            {
                throw new PermissionDeniedException();
            }

            ActivityService.AddAttendee(id, actor);
            WorkUnit.Commit();

            return View("CloseModal");
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ClassDayPartial(DateTime? date, ActivitySearchCriteriaModel model)
        {
            if (!date.HasValue) date = DateTime.Now.Date;

            return View(new ActivityClassDayPartialModel(date.Value, model));
        }

        [HttpPost]
        public ActionResult ClassDayContentPartial(DateTime? date, ActivitySearchCriteriaModel model)
        {
            var criteria = new ActivitySearchCriteria();

            if (model != null)
            {
                criteria.DayOfWeek = model.DayOfWeek ?? null;
                criteria.Radius = model.Radius ?? null;
                criteria.TeacherId = model.TeacherId ?? null;
                criteria.TeacherName = model.TeacherName ?? null;
                criteria.TimeOfDay = model.TimeOfDay ?? null;
                criteria.TypeId = model.TypeId ?? null;
                criteria.VenueId = model.VenueId ?? null;
                criteria.VenueName = model.VenueName ?? null;
                criteria.ConditionId = model.ConditionId ?? null;
            }

            model.PopulateEntity(criteria);

            if (!date.HasValue) date = DateTime.Now.Date;

            criteria.EarliestTime = date;
            criteria.LatestTime = date.Value.AddDays(1);

            var repeats = ActivityService.SearchClasses(criteria);

            return View(new ActivityClassDayContentPartialModel(date.Value, model, repeats));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ClassWeekPartial(DateTime? date, ActivitySearchCriteriaModel model, bool editable = false)
        {
            if (!date.HasValue) date = DateTime.Now.Date.WeekBegin(DayOfWeek.Monday);

            return View(new ActivityClassWeekPartialModel(date.Value, model, editable));
        }

        [HttpPost]
        public ActionResult ClassWeekContentPartial(DateTime date, ActivitySearchCriteriaModel model, bool editable = false)
        {
            var criteria = new ActivitySearchCriteria();

            model.PopulateEntity(criteria);

            criteria.EarliestTime = date;
            criteria.LatestTime = date.AddDays(7);

            var repeats = ActivityService.SearchClasses(criteria);

            return View(new ActivityClassWeekContentPartialModel(date, model, repeats, editable));
        }

        public ActionResult ScheduleWeekPartial(DateTime? date, ActivitySearchCriteriaModel model, bool editable = false)
        {
            // Inital Week Overview
            if (!date.HasValue) date = DateTime.Now.Date.WeekBegin(DayOfWeek.Monday);

            var testing = new ActivityClassWeekPartialModel(date.Value, model, editable);

            return View(testing);
        }

        [HttpPost]
        public ActionResult ScheduleWeekContentPartial(DateTime date, ActivitySearchCriteriaModel model, bool editable = false)
        {
            //classes week view
            var criteria = new ActivitySearchCriteria();

            date = date.WeekBegin(DayOfWeek.Monday);

            model.PopulateEntity(criteria);
            criteria.ConfirmedActivityTeacher = true;

            criteria.EarliestTime = date;
            criteria.LatestTime = date.AddDays(7);

            var repeats = ActivityService.SearchClasses(criteria);

            return View(new ActivityClassWeekContentPartialModel(date, model, repeats, editable));
        }


        [HttpPost]
        public ActionResult ActivitesForYear(int promoterId, bool isTeacher, bool isVenue, bool isTto)
        {
            var events = ActivityService.GetEventsForYear(promoterId, isTeacher, isVenue, isTto);

            return View(new ActivityYearViewModel(events));
        }


        [HttpPost]
        public ActionResult TTOActivitesForYear(int promoterId, bool isTeacher, bool isVenue, bool isTto)
        {
            var vm = new TTOActivityYearViewModel();
            var scheduleList = new List<TTOActivityYearViewItem>();
            var ttoId = EntityService.GetEntityHandle(promoterId).EntityId;
            var events = ActivityService.GetEventsForYear(ttoId, isTeacher, isVenue, isTto);
            var ttoCourses = TeacherTrainingService.SearchCourses(
                new TeacherTrainingCourseSearchCriteria
                {
                    OrganisationId = ttoId,
                    Completed = false
                });

            foreach (var ycEvent in events)
            {
                scheduleList.Add(GetEventScheduleItem(ycEvent));
            }

            foreach (var course in ttoCourses)
            {
                scheduleList.Add(GetTTOScheduleItem(course));
            }

            vm.ScheduleItems = scheduleList.OrderBy(x => x.StartDate).ToList();

            return View(vm);
        }

        public TTOActivityYearViewItem GetEventScheduleItem(Activity ycEvent)
        {
            var scheduleItem = new TTOActivityYearViewItem
            {
                EndDate = ycEvent.FinishTime,
                Id = ycEvent.Id,
                Name = ycEvent.Name,
                StartDate = ycEvent.StartTime,
                TTOCourse = false,
                Description = ycEvent.Description
            };

            if (ycEvent.ActivityVenue.Venue != null)
            {
                scheduleItem.VenueId = ycEvent.ActivityVenue.Venue.Id;
                scheduleItem.VenueName = ycEvent.ActivityVenue.Venue.Name;

                if (ycEvent.ActivityVenue.Venue.Address != null)
                {
                    scheduleItem.VenueCity = ycEvent.ActivityVenue.Venue.Address.City;

                    if (ycEvent.ActivityVenue.Venue.Address.Country != null)
                    {
                        scheduleItem.VenueCountry = ycEvent.ActivityVenue.Venue.Address.Country.EnglishName;
                    }
                }
            }

            return scheduleItem;
        }

        public TTOActivityYearViewItem GetTTOScheduleItem(TeacherTrainingCourse course)
        {
            var scheduleItem = new TTOActivityYearViewItem
            {
                EndDate = DateTime.Parse(course.FinishDate.ToString()),
                Id = course.Id,
                Name = course.Name,
                StartDate = DateTime.Parse(course.StartDate.ToString()),
                TTOCourse = true,
                VenueId = null,
                VenueCity = "",
                VenueCountry = "",
                VenueName = "",
                Description = course.Description
            };

            if (course.Venues != null)
            {
                if (course.Venues.Any())
                {
                    var firstVenue = course.Venues.FirstOrDefault().Venue;

                    scheduleItem.VenueId = firstVenue.Id;
                    scheduleItem.VenueName = firstVenue.Name;

                    if (firstVenue.Address != null)
                    {
                        scheduleItem.VenueCity = firstVenue.Address.City;

                        if (firstVenue.Address.Country != null)
                        {
                            scheduleItem.VenueCountry = firstVenue.Address.Country.EnglishName;
                        }
                    }
                }
            }

            return scheduleItem;
        }


        [HttpPost]
        public ActionResult ClassSummaryPartial(int id)
        {
            var activity = ActivityService.GetActivityForDisplay(id);
            return View(new ActivityClassSummaryPartialModel(activity));
        }


        [HttpPost]
        public ActionResult ClassVenueSummaryPartial(int id)
        {
            var activity = ActivityService.GetActivityForDisplay(id);
            return View(new ActivityClassVenueSummaryPartialModel(activity));
        }


        [HttpPost]
        public ActionResult ClassTeacherSummaryPartial(int id)
        {
            var activity = ActivityService.GetActivityForDisplay(id);
            return View(new ActivityClassTeacherSummaryPartialModel(activity));
        }

        public ActionResult Search(ActivitySearchCriteriaModel model)
        {
            PopulateDefaultLocation(model);

            SetActivitySearchCriteria(model);

            var type = model.TypeId.HasValue && model.TypeId > 0 ? ActivityService.GetActivityType(model.TypeId.Value) : null;

            if (type != null && type.IsClass)
            {
                model.PopulateStartDate();
                return View("SearchClasses", new ActivitySearchClassesModel(model));
            }
            else
            {
                var criteria = new ActivitySearchCriteria();
                model.PopulateEntity(criteria);
                var response = ActivityService.SearchActivities(criteria);
                type = criteria.TypeId.HasValue ? ActivityService.GetActivityType(criteria.TypeId.Value) : null;
                return View(new ActivitySearchModel(response, model, type));
            }
        }


        public ActionResult Display(int id)
        {
            var activity = ActivityService.GetActivityForDisplay(id);
            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }
            return View(new ActivityDisplayModel(activity));
        }


        public ActionResult Wall(int id)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);

            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }

            return View(new ActivityWallModel(activity));
        }

        [YogaAuthorize]
        public ActionResult Invites(int id)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Invite)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);
            var friends = ActivityService.GetInvitableFriends(id);

            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }

            return View(new ActivityInvitesModel(activity).PopulateMetadata(friends));
        }


        public ActionResult Attendees(int id)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);
            var attendees = ActivityService.GetAttendees(id);

            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }

            return View(new ActivityAttendeesModel(activity, attendees));
        }


        public ActionResult Professionals(int id)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);
            var teachers = ActivityService.GetTeachers(id);

            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }

            return View(new ActivityProfessionalsModel(activity, teachers));
        }


        public ActionResult Galleries(int id)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);

            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }

            return View(new ActivityGalleriesModel(activity));
        }


        public ActionResult Gallery(int id, int galleryId)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);

            return View(new ActivityGalleryModel(activity, galleryId));
        }


        public ActionResult Videos(int id)
        {
            var permissions = ActivityService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var activity = ActivityService.GetActivityForDisplay(id);

            return View(new ActivityVideosModel(activity));
        }


        public ActionResult OwnedPartial(int profileId)
        {
            var profile = ProfileService.GetProfileForDisplay(profileId);
            return View(new ActivityOwnedPartialModel(profile));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult OwnedContentPartial(int profileId, bool future)
        {
            var profile = ProfileService.GetProfileForDisplay(profileId);
            var self = SecurityContext.Authenticated && SecurityContext.CurrentUser.Id == profile.Owner.Id;

            var criteria = new ActivitySearchCriteria
            {
                OwnerId = self ? profile.Owner.Id : (int?)null,
                Promoter = self ? (EntityReference?)null : profile.GetEntityReference(),
                EarliestTime = future ? DateTime.Now : (DateTime?)null,
                LatestTime = future ? (DateTime?)null : DateTime.Now
            };

            var response = ActivityService.SearchActivities(criteria);

            return View("EditableListContentPartial", new ActivityEditableListContentPartialModel(profile, future, response.Activities));
        }

        public ActionResult PromotingPartial(EntityReference actor)
        {
            return View(new ActivityPromotingPartialModel(actor));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult PromotingContentPartial(EntityReference actor, bool future)
        {
            var entity = EntityService.GetEntity<IActor>(actor);

            var criteria = new ActivitySearchCriteria
            {
                Promoter = actor,
                EarliestTime = future ? DateTime.Now : (DateTime?)null,
                LatestTime = future ? (DateTime?)null : DateTime.Now
            };

            var response = ActivityService.SearchActivities(criteria);

            return View("EditableListContentPartial", new ActivityEditableListContentPartialModel(entity, future, response.Activities) { HidePromoter = true });
        }

        public ActionResult HostingPartial(int venueId)
        {
            var venue = VenueService.GetVenueForDisplay(venueId);

            var criteria = new ActivitySearchCriteria
            {
                VenueId = venueId
            };

            var response = ActivityService.SearchActivities(criteria);

            return View(new ActivityHostingPartialModel(venue, response.Activities));
        }


        public ActionResult TeachingPartial(int teacherId)
        {
            var teacher = TeacherService.GetTeacherForDisplay(teacherId);

            var criteria = new ActivitySearchCriteria
            {
                TeacherId = teacherId
            };

            var response = ActivityService.SearchActivities(criteria);

            return View(new ActivityTeachingPartialModel(teacher, response.Activities));
        }

        public ActionResult AttendingPartial(EntityReference actor)
        {
            var entity = EntityService.GetEntity<IActor>(actor);
            var attendances = ActivityService.GetUpcomingAttendances(actor);

            return View(new ActivityAttendingPartialModel(entity, attendances));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Attend(int id)
        {
            ActivityService.AddAttendee(id, SecurityContext.CurrentActor);
            WorkUnit.Commit();
            return AttendanceButton(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Unattend(int id)
        {
            ActivityService.RemoveAttendee(id, SecurityContext.CurrentActor);
            WorkUnit.Commit();
            return AttendanceButton(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult InviteFriends(int id, int[] friendIds)
        {
            if (friendIds == null) return new EmptyResult();
            ActivityService.InviteFriends(id, friendIds);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult InviteFans(int id)
        {
            var entitytype = SecurityContext.CurrentActor.GetType();
            var fans = FanService.GetFans(SecurityContext.CurrentActor.Id, entitytype.BaseType.Name);

            if (fans.Any())
            {
                ActivityService.InviteFans(id);
                WorkUnit.Commit();
            }

            return Json(new { noOfFans = fans.Count() });
        }


        [YogaAuthorize]
        public ActionResult Settings(int id)
        {
            var activity = ActivityService.GetActivityForEditing(id);

            if (activity.ProfileBanner == null || activity.Image == null)
            {
                activity = GetImagesForActivity(activity);
            }

            return View(new ActivitySettingsModel(activity));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Settings(int id, ActivitySettingsModel model)
        {
            var activity = ActivityService.GetActivityForEditing(id);

            model.PopulateEntity(activity);
            WorkUnit.Commit();

            return View(model.PopuplateMetadata(activity));
        }


        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var activity = ActivityService.GetActivityForEditing(id);
            if (activity.Type.IsClass) throw new PermissionDeniedException();
            return View(new ActivityUpdateModel(activity).PopulateMetadata(activity, ActivityService, ProfileService, StyleService, MedicService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, ActivityUpdateModel model)
        {
            var activity = ActivityService.GetActivityForEditing(id);

            if (activity.Type.IsClass) throw new PermissionDeniedException();

            if (!ModelState.IsValid) return View(model.PopulateMetadata(activity, ActivityService, ProfileService, StyleService, MedicService));

            model.PopulateEntity(activity, SecurityContext, ActivityService, EntityService, ImageService, StyleService, MedicService);
            WorkUnit.Commit();

            return RedirectToAction("Display");
        }


        [YogaAuthorize]
        public ActionResult UpdateClass(int id)
        {
            var activity = ActivityService.GetActivityForEditing(id);
            if (!activity.Type.IsClass) throw new PermissionDeniedException();
            return View(new ActivityUpdateClassModel(activity).PopulateMetadata(activity, ActivityService, ProfileService, StyleService, MedicService));
        }







        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateClass(int id, ActivityUpdateClassModel model)
        {
            var activity = ActivityService.GetActivityForEditing(id);

            if (!activity.Type.IsClass) throw new PermissionDeniedException();

            if (!ModelState.IsValid) return View(model.PopulateMetadata(activity, ActivityService, ProfileService, StyleService, MedicService));

            model.PopulateEntity(activity, SecurityContext, ActivityService, EntityService, StyleService, MedicService);
            WorkUnit.Commit();

            return RedirectToAction("Display");
        }


        

        [YogaAuthorize]
        public ActionResult CloneActivity(int id)
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            var teacher = profile != null ? TeacherService.GetTeacherForUser(profile.Owner.Id) : null;
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;
            var activityModel =
                new ActivityStart(profile.Id, profile, teacher, venue, tto).PopulateMetadata(ActivityService,
                    ProfileService);

            if (activityModel.HasTeacher == false & activityModel.HasTTO == false & activityModel.HasVenue == false)
            {
                return RedirectToAction("start", "signup");
            }
            var activity = ActivityService.GetActivityForEditing(id);
           
            activityModel.ActivityCloneId = id;
            activityModel.PromoterHandle.Selection = activity.PromoterHandle.Id;
            return View("ActivitiesStart", activityModel);

      
        }



        [HttpPost]
        [YogaAuthorize]
        public ActionResult UpdateBanner(ActivitiesBannerUpdateModel model)
        {
            var activity = ActivityService.GetActivityForEditing(model.ActivityId);

            model.PopulateEntity(activity, ImageService);
            activity.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.ActivityId);
        }

        [YogaAuthorize]
        public ActionResult ChangeVenue(int id)
        {
            return View(new ActivityChangeVenueModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult ChangeVenue(int id, ActivityChangeVenueModel model)
        {
            if (!ModelState.IsValid) return View(model);

            ActivityService.SetActivityVenue(id, model.VenueId);
            WorkUnit.Commit();
            return View("CloseModal", new CloseModalModel(null, "ChangeActivityVenue"));
        }

        private DateTime GetNextDateForRepeatDay(DayOfWeek dayOfWeek)
        {
            var today = DateTime.Now;
            int daysUntilDay = ((int)dayOfWeek - (int)today.DayOfWeek + 7) % 7;
            DateTime nextDaty = today.AddDays(daysUntilDay);
            return nextDaty;
        }

        [YogaAuthorize]
        public ActionResult AddTeacher(int id)
        {
            return View(new ActivityAddTeacherModel());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult AddTeacher(int id, ActivityAddTeacherModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            if (model.Teacher.Id == null)
            {
                ModelState.AddModelError("Noteacher", "Please select a teacher");
                return View(model);
            }




            if (model.Teacher.Id.HasValue)
            {
                ActivityService.AddActivityTeacher(id, model.Teacher.Id.Value);
                WorkUnit.Commit();
            }

            return View("CloseModal", new CloseModalModel(null, "AddActivityTeacher"));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult RemoveTeacher(int id, int teacherId)
        {
            ActivityService.RemoveActivityTeacher(id, teacherId);
            WorkUnit.Commit();

            return new EmptyResult();
        }

        private ActivitySearchCriteriaModel GetActivitySearchCriteria()
        {
            return Session["ActivitySearchCriteria"] as ActivitySearchCriteriaModel ?? new ActivitySearchCriteriaModel();
        }

        private void SetActivitySearchCriteria(ActivitySearchCriteriaModel criteria)
        {
            Session["ActivitySearchCriteria"] = criteria;
        }

        private ActivitySearchCriteriaModel PopulateDefaultLocation(ActivitySearchCriteriaModel model)
        {
            if (!SecurityContext.Authenticated) return model;
            if (SecurityContext.CurrentProfile.Location == null) return model;
            if (model.Latitude.HasValue || model.Longitude.HasValue) return model;

            model.Location = SecurityContext.CurrentProfile.Location.Name;
            model.Latitude = SecurityContext.CurrentProfile.Location.Geography.Latitude;
            model.Longitude = SecurityContext.CurrentProfile.Location.Geography.Longitude;
            //model.Radius = 10000;
            //model.SortOrder = ActivitySortOrder.Distance;

            return model;
        }

        private Activity GetImagesForActivity(Activity activity)
        {
            if (activity.PromoterHandle != null)
            {
                if (activity.PromoterHandle.EntityType != null)
                {
                    if (activity.PromoterHandle.EntityType.Name == "TeacherTrainingOrganisation")
                    {
                        var tto = TeacherTrainingService.GetOrganisation(activity.PromoterHandle.EntityId);
                        if (tto.ProfileBanner != null && activity.ProfileBanner == null) { activity.ProfileBanner = tto.ProfileBanner; }
                        if (activity.Image == null && tto.Image != null) { activity.Image = tto.Image; }
                    }

                    if (activity.PromoterHandle.EntityType.Name == "Teacher")
                    {
                        var teacher = TeacherService.GetTeacher(activity.PromoterHandle.EntityId);
                        if (teacher.ProfileBanner != null && activity.ProfileBanner == null) { activity.ProfileBanner = teacher.ProfileBanner; }
                        if (activity.Image == null && teacher.Image != null) { activity.Image = teacher.Image; }
                    }

                    if (activity.PromoterHandle.EntityType.Name == "Venue")
                    {
                        var venue = VenueService.GetVenue(activity.PromoterHandle.EntityId);
                        if (venue.ProfileBanner != null && activity.ProfileBanner == null) { activity.ProfileBanner = venue.ProfileBanner; }
                        if (activity.Image == null && venue.Image != null) { activity.Image = venue.Image; }
                    }
                }
            }

            return activity;
        }
        
        [YogaAuthorize]
        [HttpGet]
        public ActionResult Activities()
        {
            Profile profile = this.SecurityContext.CurrentProfile;

            var teacher = profile != null ? TeacherService.GetTeacherForUser(profile.Owner.Id) : null;
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;
            var activityModel =
                new ActivityStart(profile.Id, profile, teacher, venue, tto).PopulateMetadata(ActivityService,
                    ProfileService);

            if (activityModel.HasTeacher == false & activityModel.HasTTO == false & activityModel.HasVenue == false)
            {
                return RedirectToAction("start", "signup");
            }

            return View("ActivitiesStart", activityModel);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateActivity(ActivityStart model)
        {

     

            if (!ModelState.IsValid) return View(model.PopulateMetadata(ActivityService, ProfileService));

            Profile profile = this.SecurityContext.CurrentProfile;

            var teacher = profile != null ? TeacherService.GetTeacherForUser(profile.Owner.Id) : null;
            var venue = profile != null ? VenueService.GetVenueForUser(profile.Owner.Id) : null;
            var tto = profile != null ? TeacherTrainingService.GetOrganisationForUser(profile.Owner.Id) : null;

            var type = ActivityService.GetActivityType(model.Type.Id);
            var classModel = new ActivityCreateViewModel(profile, teacher, venue, tto);

            classModel.ActivityTypeName = model.Type.Id != 1 ? "Activity name" : "Class name";
            ViewBag.TypeOfPage = model.Type.Id != 1 ? "activity" : "class";
            if (model.ActivityCloneId != 0)
            {
                var activity = ActivityService.GetActivityForEditing(model.ActivityCloneId);
                classModel.PopulateMetadata(type.IsClass, ActivityService, StyleService, MedicService, AddressService.GetCountries(), model.PromoterHandle, model.Type);

                classModel.PopulateMetadataClone(activity, ActivityService, StyleService, MedicService, TeacherService, AddressService.GetCountries());

            }
            else
            {
                classModel.PopulateMetadata(type.IsClass, ActivityService, StyleService, MedicService, AddressService.GetCountries(), model.PromoterHandle, model.Type);
 
            }
            
            return View("ActivitiesCreate", classModel);
        }

        [HttpPost]
        public ActionResult CreateEvent(ActivityCreateViewModel model)
        {
            Venue venue;
            var newVenueId = 0;
            //No Id = new venue needed
            if (model.Venue.Id == null)
            {
                if (model.Venue.Owned)
                {
                    venue = VenueService.CreateVenue(SecurityContext.CurrentUser);
                    venue.Name = model.Name;
                    venue.EmailAddress = SecurityContext.CurrentUser.EmailAddress;
                    venue.Address = model.Venue.PopulateEntity(AddressService);
                    WorkUnit.Commit();
                    model.Venue.Id = venue.Id;
                }
                else
                {
                    venue = VenueService.CreateStubbed(model.Venue.Name);
                    venue.Address = model.Venue.PopulateEntity(AddressService);
                    WorkUnit.Commit();
                    var invite = EntityService.InviteEntity(venue, SecurityContext.CurrentUser, model.Venue.Email);
                    model.Venue.Id = venue.Id;
                }
            }
            else
            {
                venue = VenueService.GetVenue(model.Venue.Id.Value);
            }

            int returnActivityId = 0;

            if (model.RepeatMonday || model.RepeatTuesday || model.RepeatWednesday || model.RepeatThursday || model.RepeatFriday || model.RepeatSaturday || model.RepeatSunday)
            {
                returnActivityId = CreateWeeklyRepeats(model);
            }
            else
            {
                var activity = new Activity { Owner = SecurityContext.CurrentUser };
                model.PopulateEntity(activity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);
                ActivityService.CreateActivity(activity);
                ActivityService.SetClassServices(activity);
                WorkUnit.Commit();
                returnActivityId = activity.Id;
            }

            WorkUnit.Commit();
            return RedirectToAction("Display", "Activities", new { id = returnActivityId });

        }

        private int CreateWeeklyRepeats(ActivityCreateViewModel createModel)
        {
            var today = DateTime.Now;
            var returnActivityId = 0;
            if (createModel.RepeatMonday)
            {
                var mondayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(mondayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextMonday = today.DayOfWeek == DayOfWeek.Monday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Monday);
                mondayActivity.StartTime = nextMonday.Date + createModel.TimeRange.StartTimeSpan.Value;
                mondayActivity.FinishTime = nextMonday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(mondayActivity);
                ActivityService.SetClassServices(mondayActivity);
                WorkUnit.Commit();
                returnActivityId = mondayActivity.Id;
            }

            if (createModel.RepeatTuesday)
            {
                var tuesdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(tuesdayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextTuesday = today.DayOfWeek == DayOfWeek.Tuesday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Tuesday);
                tuesdayActivity.StartTime = nextTuesday.Date + createModel.TimeRange.StartTimeSpan.Value;
                tuesdayActivity.FinishTime = nextTuesday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(tuesdayActivity);
                ActivityService.SetClassServices(tuesdayActivity);
                WorkUnit.Commit();
                returnActivityId = tuesdayActivity.Id;
            }

            if (createModel.RepeatWednesday)
            {
                var wednesdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(wednesdayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextWednesday = today.DayOfWeek == DayOfWeek.Wednesday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Wednesday);
                wednesdayActivity.StartTime = nextWednesday.Date + createModel.TimeRange.StartTimeSpan.Value;
                wednesdayActivity.FinishTime = nextWednesday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(wednesdayActivity);
                ActivityService.SetClassServices(wednesdayActivity);
                WorkUnit.Commit();
                returnActivityId = wednesdayActivity.Id;
            }

            if (createModel.RepeatThursday)
            {
                var thursdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(thursdayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextThursday = today.DayOfWeek == DayOfWeek.Thursday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Thursday);
                thursdayActivity.StartTime = nextThursday.Date + createModel.TimeRange.StartTimeSpan.Value;
                thursdayActivity.FinishTime = nextThursday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(thursdayActivity);
                ActivityService.SetClassServices(thursdayActivity);
                WorkUnit.Commit();
                returnActivityId = thursdayActivity.Id;
            }

            if (createModel.RepeatFriday)
            {
                var fridayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(fridayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextFriday = today.DayOfWeek == DayOfWeek.Friday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Friday);
                fridayActivity.StartTime = nextFriday.Date + createModel.TimeRange.StartTimeSpan.Value;
                fridayActivity.FinishTime = nextFriday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(fridayActivity);
                ActivityService.SetClassServices(fridayActivity);
                WorkUnit.Commit();
                returnActivityId = fridayActivity.Id;
            }

            if (createModel.RepeatSaturday)
            {
                var saturdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(saturdayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextSaturday = today.DayOfWeek == DayOfWeek.Saturday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Saturday);
                saturdayActivity.StartTime = nextSaturday.Date + createModel.TimeRange.StartTimeSpan.Value;
                saturdayActivity.FinishTime = nextSaturday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(saturdayActivity);
                ActivityService.SetClassServices(saturdayActivity);
                WorkUnit.Commit();
                returnActivityId = saturdayActivity.Id;
            }

            if (createModel.RepeatSunday)
            {
                var sundayActivity = new Activity { Owner = SecurityContext.CurrentUser };
                createModel.PopulateEntity(sundayActivity, ActivityService, StyleService, MedicService, TeacherService, VenueService, EntityService, SecurityContext);

                var nextSunday = today.DayOfWeek == DayOfWeek.Sunday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Sunday);
                sundayActivity.StartTime = nextSunday.Date + createModel.TimeRange.StartTimeSpan.Value;
                sundayActivity.FinishTime = nextSunday.Date + createModel.TimeRange.FinishTimeSpan.Value;
                ActivityService.CreateActivity(sundayActivity);
                ActivityService.SetClassServices(sundayActivity);

                WorkUnit.Commit();
                returnActivityId = sundayActivity.Id;
            }

            return returnActivityId;
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult VenueInvite(string emailAddress, string venueName, string fromName)
        {
            try
            {
                var toemailarray = emailAddress.Split(',');

                foreach (var email in toemailarray)
                {
                    new InviteCongrats
                    {
                        InviteToEmail = email,
                        InviteFromName = fromName,
                        InviteToName = venueName

                    }.Send();
                }

                return Content("true");
            }
            catch (Exception)
            {
                return Content("false");
            }
        }

        // OLD CREATE


        //[YogaAuthorize]
        //public ActionResult Create(int ownerId)
        //{
        //    return View("CreateStep1", new ActivityCreateStep1Model(ownerId).PopulateMetadata(ActivityService, ProfileService));
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateStep1(ActivityCreateStep1Model model)
        //{
        //    if (!ModelState.IsValid) return View(model.PopulateMetadata(ActivityService, ProfileService));

        //    var type = ActivityService.GetActivityType(model.Type.Id);

        //    if (type.IsClass)
        //    {
        //        var createModel = new ActivityCreateClassModel();
        //        ViewBag.CreateModel = createModel;
        //        ViewBag.ActivityTypeName = type.Name;
        //        createModel.Step1Model = model;
        //        return View("CreateClassStep2", new ActivityCreateClassStep2Model().PopulateMetadata(ActivityService));
        //    }
        //    else
        //    {
        //        var createModel = new ActivityCreateModel();
        //        createModel.ActivityTypeName = ActivityService.GetActivityType(model.Type.Id).Name;
        //        ViewBag.CreateModel = createModel;
        //        ViewBag.ActivityTypeName = type.Name;
        //        createModel.Step1Model = model;
        //        return View("CreateStep2", new ActivityCreateStep2Model().PopulateMetadata(StyleService, MedicService));
        //    }
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateStep2(ActivityCreateStep2Model model, string createModel, string createmodel2)
        //{
        //    var devtemp = new ActivityCreateModel();

        //    if (String.IsNullOrEmpty(createModel))
        //    {
        //        devtemp = (ActivityCreateModel)new MvcSerializer().Deserialize(createmodel2, SerializationMode.Signed);
        //        devtemp.Step2Model = model;
        //        ViewBag.CreateModel3 = devtemp;
        //    }
        //    else
        //    {
        //        devtemp = (ActivityCreateModel)new MvcSerializer().Deserialize(createModel, SerializationMode.Signed);
        //        devtemp.Step2Model = model;
        //        ViewBag.CreateModel3 = devtemp;
        //    }

        //    ViewBag.CreateModel2 = devtemp;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel = devtemp;
        //        ModelState.Clear();
        //        return View("CreateStep1", devtemp.Step1Model.PopulateMetadata(ActivityService, ProfileService));
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CreateModel = devtemp;
        //        return View(model.PopulateMetadata(StyleService, MedicService));
        //    }
        //    devtemp.Step2Model = model;
        //    return View("CreateStep3", new ActivityCreateStep3Model());
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateStep3(ActivityCreateStep3Model model, string createModel2, string createModel3)
        //{
        //    var devtemp = new ActivityCreateModel();

        //    if (String.IsNullOrEmpty(createModel2))
        //    {
        //        devtemp = (ActivityCreateModel)new MvcSerializer().Deserialize(createModel3, SerializationMode.Signed);
        //        devtemp.Step3Model = model;
        //        ViewBag.CreateModel3 = devtemp;
        //    }
        //    else
        //    {
        //        devtemp = (ActivityCreateModel)new MvcSerializer().Deserialize(createModel2, SerializationMode.Signed);
        //        devtemp.Step3Model = model;
        //        ViewBag.CreateModel3 = devtemp;
        //    }


        //    ViewBag.CreateModel3 = devtemp;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel = devtemp;
        //        ViewBag.CreateModel2 = devtemp;
        //        ModelState.Clear();
        //        return View("CreateStep2", devtemp.Step2Model.PopulateMetadata(StyleService, MedicService));
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CreateModel2 = devtemp;
        //        return View(model);
        //    }
        //    devtemp.Step3Model = model;
        //    devtemp.Step4Model = new ActivityCreateStep4Model();
        //    return View("CreateStep4", new ActivityCreateStep4Model());
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateStep4(ActivityCreateStep4Model model, string createModel3)
        //{

        //    var devtemp = (ActivityCreateModel)new MvcSerializer().Deserialize(createModel3, SerializationMode.Signed);
        //    ViewBag.CreateModel4 = devtemp;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel2 = devtemp;
        //        ModelState.Clear();
        //        return View("CreateStep3", devtemp.Step3Model.PopulateMetadata(TeacherService));
        //    }

        //    if (!ModelState.IsValid) 
        //    {
        //        ViewBag.CreateModel3 = devtemp;
        //       return View(model);
        //    }
        //    devtemp.Step4Model = model;

        //    var activity = new Activity { Owner = SecurityContext.CurrentUser };
        //    devtemp.PopulateEntity(activity, SecurityContext, ActivityService, EntityService, TeacherService, VenueService, StyleService, MedicService);

        //    ActivityService.CreateActivity(activity);
        //    WorkUnit.Commit();

        //    return View("CloseModal", new CloseModalModel(Url.Action( activity.Id.ToString(), "Activities"), "CreateActivity"));
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateClassStep2(ActivityCreateClassStep2Model model, string createModel, string createmodel02)
        //{

        //    var devtemp = new ActivityCreateClassModel();

        //    if (String.IsNullOrEmpty(createModel))
        //    {
        //        devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createmodel02, SerializationMode.Signed);
        //        devtemp.Step2Model = model;
        //        ViewBag.CreateModel02 = devtemp;
        //    }
        //    else
        //    {
        //        devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createModel, SerializationMode.Signed);
        //        devtemp.Step2Model = model;
        //        ViewBag.CreateModel02 = devtemp;
        //    }


        //  //  ViewBag.CreateModel02 = devtemp;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel = devtemp;
        //        ModelState.Clear();
        //        return View("CreateStep1", devtemp.Step1Model.PopulateMetadata(ActivityService, ProfileService));
        //    }

        //    if (!ModelState.IsValid) 
        //    {
        //        ViewBag.CreateModel = devtemp;
        //      return  View(model.PopulateMetadata(ActivityService));
        //    }
        //    devtemp.Step2Model = model;
        //    return View("CreateClassStep3", new ActivityCreateClassStep3Model().PopulateMetadata(StyleService, MedicService));
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateClassStep3(ActivityCreateClassStep3Model model, string createModel02, string createModel03)
        //{

        //    var devtemp = new ActivityCreateClassModel();

        //    if (String.IsNullOrEmpty(createModel02))
        //    {
        //        devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createModel03, SerializationMode.Signed);
        //        devtemp.Step3Model = model;
        //        ViewBag.CreateModel03 = devtemp;
        //    }
        //    else
        //    {
        //        devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createModel02, SerializationMode.Signed);
        //        devtemp.Step3Model = model;
        //        ViewBag.CreateModel03 = devtemp;
        //    }


        //  //  ViewBag.CreateModel03 = devtemp;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel = devtemp;
        //        ViewBag.CreateModel02 = devtemp;
        //        ModelState.Clear();
        //        return View("CreateClassStep2", devtemp.Step2Model.PopulateMetadata(ActivityService));
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CreateModel02 = devtemp;
        //        return View(model.PopulateMetadata(StyleService, MedicService));
        //    }
        //    devtemp.Step3Model = model;
        //    return View("CreateClassStep4", new ActivityCreateClassStep4Model());
        //}


        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateClassStep4(ActivityCreateClassStep4Model model, string createModel03, string createModel04)
        //{

        //    var devtemp = new ActivityCreateClassModel();

        //    if (String.IsNullOrEmpty(createModel03))
        //    {
        //        devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createModel04, SerializationMode.Signed);
        //        devtemp.Step4Model = model;
        //        ViewBag.CreateModel04 = devtemp;
        //    }
        //    else
        //    {
        //        devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createModel03, SerializationMode.Signed);
        //        devtemp.Step4Model = model;
        //        ViewBag.CreateModel04 = devtemp;
        //    }

        //  //  ViewBag.CreateModel04 = devtemp;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel02 = devtemp;
        //        ViewBag.CreateModel03 = devtemp;
        //        ModelState.Clear();
        //        return View("CreateClassStep3", devtemp.Step3Model.PopulateMetadata(StyleService, MedicService));
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CreateModel03 = devtemp;
        //        return View(model);
        //    }
        //    devtemp.Step4Model = model;
        //    return View("CreateClassStep5", new ActivityCreateClassStep5Model());
        //}

        //[HttpPost]
        //[YogaAuthorize]
        //public ActionResult CreateClassStep5(ActivityCreateClassStep5Model model, string createModel04)
        //{

        //    var devtemp = (ActivityCreateClassModel)new MvcSerializer().Deserialize(createModel04, SerializationMode.Signed);
        //   // ViewBag.CreateModel05 = devtemp;


        //    var returnActivityId = 0;

        //    if (model.Back)
        //    {
        //        ViewBag.CreateModel03 = devtemp;
        //        ViewBag.CreateModel04 = devtemp;
        //        ModelState.Clear();
        //        return View("CreateClassStep4", devtemp.Step4Model);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.CreateModel04 = devtemp;
        //        return View(model);
        //    }
        //    devtemp.Step5Model = model;

        //    if (devtemp.Step2Model.RepeatMonday || devtemp.Step2Model.RepeatTuesday || devtemp.Step2Model.RepeatWednesday || devtemp.Step2Model.RepeatThursday ||
        //        devtemp.Step2Model.RepeatFriday || devtemp.Step2Model.RepeatSaturday || devtemp.Step2Model.RepeatSunday)
        //    {
        //        returnActivityId = CreateWeeklyRepeats(devtemp);
        //    }
        //    else
        //    {
        //        var activity = new Activity { Owner = SecurityContext.CurrentUser };
        //        devtemp.PopulateEntity(activity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);
        //        ActivityService.CreateActivity(activity);
        //        ActivityService.SetClassServices(activity);
        //        WorkUnit.Commit();
        //        returnActivityId = activity.Id;
        //    }

        //    return View("CloseModal", new CloseModalModel(Url.Action( returnActivityId.ToString(),"Activities"), "CreateClass"));
        //}

        //private int CreateWeeklyRepeats(ActivityCreateClassModel createModel)
        //{
        //    var today = DateTime.Now;
        //    var returnActivityId = 0;
        //    if (createModel.Step2Model.RepeatMonday)
        //    {
        //        var mondayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(mondayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextMonday = today.DayOfWeek == DayOfWeek.Monday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Monday);
        //        mondayActivity.StartTime = nextMonday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        mondayActivity.FinishTime = nextMonday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(mondayActivity);
        //        ActivityService.SetClassServices(mondayActivity);
        //        WorkUnit.Commit();
        //        returnActivityId = mondayActivity.Id;
        //    }

        //    if (createModel.Step2Model.RepeatTuesday)
        //    {
        //        var tuesdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(tuesdayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextTuesday = today.DayOfWeek == DayOfWeek.Tuesday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Tuesday);
        //        tuesdayActivity.StartTime = nextTuesday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        tuesdayActivity.FinishTime = nextTuesday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(tuesdayActivity);
        //        ActivityService.SetClassServices(tuesdayActivity);
        //        WorkUnit.Commit();
        //        returnActivityId = tuesdayActivity.Id;
        //    }

        //    if (createModel.Step2Model.RepeatWednesday)
        //    {
        //        var wednesdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(wednesdayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextWednesday = today.DayOfWeek == DayOfWeek.Wednesday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Wednesday);
        //        wednesdayActivity.StartTime = nextWednesday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        wednesdayActivity.FinishTime = nextWednesday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(wednesdayActivity);
        //        ActivityService.SetClassServices(wednesdayActivity);
        //        WorkUnit.Commit();
        //        returnActivityId = wednesdayActivity.Id;
        //    }

        //    if (createModel.Step2Model.RepeatThursday)
        //    {
        //        var thursdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(thursdayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextThursday = today.DayOfWeek == DayOfWeek.Thursday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Thursday);
        //        thursdayActivity.StartTime = nextThursday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        thursdayActivity.FinishTime = nextThursday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(thursdayActivity);
        //        ActivityService.SetClassServices(thursdayActivity);
        //        WorkUnit.Commit();
        //        returnActivityId = thursdayActivity.Id;
        //    }

        //    if (createModel.Step2Model.RepeatFriday)
        //    {
        //        var fridayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(fridayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextFriday = today.DayOfWeek == DayOfWeek.Friday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Friday);
        //        fridayActivity.StartTime = nextFriday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        fridayActivity.FinishTime = nextFriday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(fridayActivity);
        //        ActivityService.SetClassServices(fridayActivity);
        //        WorkUnit.Commit();
        //        returnActivityId = fridayActivity.Id;
        //    }

        //    if (createModel.Step2Model.RepeatSaturday)
        //    {
        //        var saturdayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(saturdayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextSaturday = today.DayOfWeek == DayOfWeek.Saturday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Saturday);
        //        saturdayActivity.StartTime = nextSaturday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        saturdayActivity.FinishTime = nextSaturday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(saturdayActivity);
        //        ActivityService.SetClassServices(saturdayActivity);
        //        WorkUnit.Commit();
        //        returnActivityId = saturdayActivity.Id;
        //    }

        //    if (createModel.Step2Model.RepeatSunday)
        //    {
        //        var sundayActivity = new Activity { Owner = SecurityContext.CurrentUser };
        //        createModel.PopulateEntity(sundayActivity, SecurityContext, ActivityService, EntityService, StyleService, TeacherService, VenueService, MedicService);

        //        var nextSunday = today.DayOfWeek == DayOfWeek.Sunday ? DateTime.Now : GetNextDateForRepeatDay(DayOfWeek.Sunday);
        //        sundayActivity.StartTime = nextSunday.Date + createModel.Step2Model.TimeRange.StartTimeSpan.Value;
        //        sundayActivity.FinishTime = nextSunday.Date + createModel.Step2Model.TimeRange.FinishTimeSpan.Value;
        //        ActivityService.CreateActivity(sundayActivity);
        //        ActivityService.SetClassServices(sundayActivity);

        //        WorkUnit.Commit();
        //        returnActivityId = sundayActivity.Id;
        //    }

        //    return returnActivityId;
        //}







    }
}
