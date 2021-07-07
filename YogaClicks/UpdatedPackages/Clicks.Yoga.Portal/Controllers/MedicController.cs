using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Follow;
using Clicks.Yoga.Portal.Models.Medic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clicks.Yoga.Portal.Controllers
{

    public class MedicController : YogaController
    {
        public MedicController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IMedicService medicService,
            ITeacherService teacherService,
            IVenueService venueService,
            IImageService imageService,
            IFollowService followService,
            IEntityService entityService,
            INotificationService notificationService,
            IAccountService accountService
        )
            : base(workUnit, securityContext)
        {
            MedicService = medicService;
            TeacherService = teacherService;
            VenueService = venueService;
            ImageService = imageService;
            FollowService = followService;
            EntityService = entityService;
            NotificationService = notificationService;
            AccountService = accountService;
        }

        private IMedicService MedicService { get; set; }
        private ITeacherService TeacherService { get; set; }
        private IVenueService VenueService { get; set; }
        private IImageService ImageService { get; set; }
        private IFollowService FollowService { get; set; }
        private IEntityService EntityService { get; set; }
        private INotificationService NotificationService { get; set; }
        private IAccountService AccountService { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OurMission()
        {
            return View();
        }

        public ActionResult MedicNavigation()
        {
            return View(new ConditionsModel(MedicService.GetConditions()));
        }

        public ActionResult MedicsPartial(int skip = 0, int take = -1)
        {
            if (take == -1) take = int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.InfiniteScroll.DefaultNumberOfStyles"]);
            var conditions = MedicService.GetConditionsForInfiniteScroll(skip, take);
            var model = new ConditionsModel(conditions);

            return View(model);
        }

        public ActionResult WhatTheScienceSays(int conditionId)
        {
            var wtss = MedicService.GetWhatTheScienceSaysForCondition(conditionId);
            var model = new WhatTheScienceSaysesModel(wtss);

            return View(model);
        }

        public ActionResult Studies(int conditionId)
        {
            var studies = MedicService.GetStudiesForCondition(conditionId);
            var model = new StudiesModel(studies);

            return View(model);
        }

        public ActionResult UserStories(int conditionId)
        {
            var userStories = MedicService.GetUserStoriesForCondition(conditionId);
            var model = new UserStoriesModel(userStories);

            return View(model);
        }

        public ActionResult Display(int id)
        {
            var condition = MedicService.GetCondition(id);

            if (condition == null) throw new UnknownEntityException();

            var vm = new ConditionDisplayModel { Condition = new ConditionModel(condition) };

            return View(vm);
        }

        public ActionResult FollowButton(int Id)
        {
            var model = new FollowButtonModel
            {
                IsCurrentlyFollowing = FollowService.IsConditionFollower(
                    SecurityContext.CurrentActor.OwnerId.Value, SecurityContext.CurrentActor.Owner.GetEntityTypeName(), Id),
                EntityId = Id
            };

            return PartialView("FollowButton", model);
        }

        [HttpPost]
        public ActionResult FollowButton(int id, bool addFollow)
        {
            var condition = MedicService.GetConditionForEdit(id);

            if (addFollow)
            {
                condition.Followers.Add(new Follow
                {
                    FollowerId = SecurityContext.CurrentActor.OwnerId.Value,
                    FollowerType = EntityService.GetEntityType(SecurityContext.CurrentActor.Owner.GetEntityTypeName())
                });
            }
            else
            {
                condition.Followers.Remove(
                    FollowService.GetFollowerForCondition(
                    id,
                    SecurityContext.CurrentActor.OwnerId.Value,
                    SecurityContext.CurrentActor.Owner.GetEntityTypeName()));
            }

            WorkUnit.Commit();

            return PartialView("FollowButton", new FollowButtonModel
            {
                EntityId = id,
                IsCurrentlyFollowing = addFollow
            });
        }

        public ActionResult TellUs(int conditionId)
        {
            return View(new TellUsModel { ConditionId = conditionId });
        }

        [HttpPost]
        public ActionResult TellUs(TellUsModel model)
        {
            var conditions = new List<Condition>();
            conditions.Add(MedicService.GetCondition(model.ConditionId));

            var teachers = new List<Teacher>();
            var venues = new List<Venue>();

            if (model.Teachers != null)
            {
                var includedTeachers = TeacherService.GetTeachers(model.Teachers.Ids.ToList());

                foreach (var teacher in includedTeachers)
                {
                    teachers.Add(teacher);
                }
            }

            if (model.Venues != null)
            {
                var includedVenues = VenueService.GetVenues(model.Venues.Ids.ToList());

                foreach (var venue in includedVenues)
                {
                    venues.Add(venue);
                }
            }

            var userStory = new UserStory
            {
                Active = true,
                Conditions = conditions,
                Content = model.Content,
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now,
                Owner = SecurityContext.CurrentUser,
                OwnerHidden = model.OwnerHidden,
                Teachers = teachers,
                Title = model.Title,
                Venues = venues
            };

            MedicService.AddUserStory(userStory);

            var followers = FollowService.GetFollowersForCondition(model.ConditionId);

            if (followers.Any())
            {
                var conditionHandle = new EntityHandle
                {
                    Active = true,
                    CreationTime = DateTime.Now,
                    EntityId = model.ConditionId,
                    EntityType = EntityService.GetEntityType("Condition"),
                    Id = model.ConditionId,
                    Name = MedicService.GetCondition(model.ConditionId).Name
                };

                foreach (var follower in followers)
                {
                    var user = AccountService.GetUser(follower.FollowerId);

                    NotificationService.Notify(user, "ConditionUserStoryFollowerUpdate", user.Profile, conditionHandle);
                }
            }


            WorkUnit.Commit();

            return View("StorySent");
        }

        [YogaAuthorize]
        public ActionResult Update(int Id)
        {
            var model = new ConditionUpdateModel(MedicService.GetConditionForEdit(Id));

            return View(model);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateBanner(ConditionBannerUpdateModel model)
        {
            var condition = MedicService.GetConditionForEdit(model.ConditionId);
            model.PopulateEntity(condition, ImageService);
            condition.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.ConditionId);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateDirectoryImage(ConditionDirectoryImageUpdateModel model)
        {
            var condition = MedicService.GetConditionForEdit(model.ConditionId);
            model.PopulateEntity(condition, ImageService);
            condition.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Update", model.ConditionId);
        }

        [YogaAuthorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateDescription(ConditionDescriptionModel model)
        {
            var condition = MedicService.GetConditionForEdit(model.Id);
            model.PopulateEntity(condition);
            condition.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return PartialView(new ConditionDescriptionModel(condition));
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult RemoveWTSSFromCondition(int conditionId, int wtssId)
        {
            var condition = MedicService.GetConditionForEdit(conditionId);
            condition.WhatTheScienceSayses
                .Remove(condition.WhatTheScienceSayses.Where(x => x.Id == wtssId)
                .FirstOrDefault());
            WorkUnit.Commit();

            return Json("success");
        }

        [YogaAuthorize]
        public ActionResult UpdateWTSS(ConditionWTSSModel model)
        {
            var allTherapyTypes = MedicService.GetTherapyTypes();
            var therapyTypes = new List<TherapyTypeModel>();

            foreach (var type in allTherapyTypes)
            {
                therapyTypes.Add(new TherapyTypeModel(type));
            }

            model.TherapyTypes = therapyTypes;

            return PartialView(model);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult AddWTSSToCondition(int conditionId, int wtssType, string wtssText)
        {
            var condition = MedicService.GetConditionForEdit(conditionId);
            var therapyTypes = new List<TherapyType>();
            therapyTypes.Add(MedicService.GetTherapyType(wtssType));

            condition.WhatTheScienceSayses.Add(new WhatTheScienceSays
            {
                Active = true,
                CreationTime = DateTime.Now,
                Description = wtssText,
                TherapyTypes = therapyTypes
            });

            WorkUnit.Commit();

            return Json("Success");
        }

        [YogaAuthorize]
        public ActionResult AddStudy(int conditionId)
        {
            var allTherapyTypes = MedicService.GetTherapyTypes();
            var therapyTypesModel = new List<TherapyTypeModel>();

            foreach (var type in allTherapyTypes)
            {
                therapyTypesModel.Add(new TherapyTypeModel(type));
            }

            return View(new AddStudyModel
            {
                ConditionId = conditionId,
                TherapyTypes = therapyTypesModel
            });
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult AddStudy(AddStudyModel study, bool yoga, bool mindfulness, bool meditation, string author)
        {
            var authors = new List<Author>();
            authors.Add(new Author { Active = true, Name = author });

            var therapysUsed = new List<string>();
            if (yoga) { therapysUsed.Add("Yoga"); }
            if (mindfulness) { therapysUsed.Add("Mindfulness"); }
            if (meditation) { therapysUsed.Add("Meditation"); }

            var conditions = new List<Condition>();
            conditions.Add(MedicService.GetCondition(study.ConditionId));

            var newStudy = new Study
            {
                Abstract = study.Abstract,
                Active = true,
                Authors = authors,
                Conditions = conditions,
                DateOfStudy = study.DateOfStudy,
                Institution = study.Institution,
                Journal = study.Journal,
                NumberOfCitations = study.NumberOfCitations,
                Source = study.Source,
                TherapyTypes = MedicService.GetTherapyTypesByNames(therapysUsed),
                Title = study.Title,
                Volume = study.Volume
            };

            MedicService.AddStudy(newStudy);

            var followers = FollowService.GetFollowersForCondition(study.ConditionId);

            if (followers.Any())
            {
                var conditionHandle = new EntityHandle
                {
                    Active = true,
                    CreationTime = DateTime.Now,
                    EntityId = study.ConditionId,
                    EntityType = EntityService.GetEntityType("Condition"),
                    Id = study.ConditionId,
                    Name = MedicService.GetCondition(study.ConditionId).Name
                };

                foreach (var follower in followers)
                {
                    var user = AccountService.GetUser(follower.FollowerId);

                    NotificationService.Notify(user, "ConditionFollowerUpdate", user.Profile, conditionHandle);
                }
            }

            WorkUnit.Commit();

            return View("StudyAdded");
        }

        [YogaAuthorize]
        public ActionResult UpdateStudy(int Id, int conditionId)
        {
            var study = MedicService.GetStudyForEdit(Id);
            var model = new UpdateStudyModel(study, conditionId);

            return View(model);
        }

        [YogaAuthorize]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateStudyDetails(StudyDetailsUpdateModel model)
        {
            var study = MedicService.GetStudy(model.Id);
            model.PopulateEntity(study);
            study.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return PartialView(new StudyDetailsUpdateModel(new StudyModel(study)));
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateStudyTherapyTypes(StudyTherapyTypesUpdateModel model, bool yoga, bool mindfulness, bool meditation)
        {
            var study = MedicService.GetStudy(model.Id);
            var allTherapyTypes = MedicService.GetTherapyTypes();
            bool currentYoga = false;
            bool currentMindfulness = false;
            bool currentMeditation = false;

            foreach (var type in study.TherapyTypes)
            {
                if (type.Name == "Yoga") { currentYoga = true; }
                if (type.Name == "Mindfulness") { currentMindfulness = true; }
                if (type.Name == "Meditation") { currentMeditation = true; }
            }

            if (currentYoga != yoga) { study = CheckTherapyChanges(currentYoga, yoga, study, allTherapyTypes, "Yoga"); }
            if (currentMindfulness != mindfulness) { study = CheckTherapyChanges(currentMindfulness, mindfulness, study, allTherapyTypes, "Mindfulness"); }
            if (currentMeditation != meditation) { study = CheckTherapyChanges(currentMeditation, meditation, study, allTherapyTypes, "Meditation"); }

            study.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return PartialView(new StudyTherapyTypesUpdateModel(new StudyModel(study)));
        }

        private Study CheckTherapyChanges(bool currentStatus, bool newStatus, Study study, List<TherapyType> allTherapyTypes, string typeName)
        {
            var type = allTherapyTypes.Where(x => x.Name == typeName).FirstOrDefault();
            if (currentStatus && !newStatus) { study.TherapyTypes.Remove(type); }
            if (!currentStatus && newStatus) { study.TherapyTypes.Add(type); }
            return study;
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult RemoveAuthorFromCondition(int studyId, int authorId)
        {
            var study = MedicService.GetStudyForEdit(studyId);
            study.Authors
                .Remove(study.Authors.Where(x => x.Id == authorId)
                .FirstOrDefault());
            WorkUnit.Commit();

            return Json("success");
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult AddAuthorToCondition(int studyId, string authorText)
        {
            var study = MedicService.GetStudyForEdit(studyId);

            study.Authors
               .Add(new Author
               {
                   Name = authorText,
                   Active = true,
                   CreationTime = DateTime.Now,
                   ModificationTime = DateTime.Now
               });

            WorkUnit.Commit();

            return Json("Success");
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult RemoveStudyFromCondition(int studyId, int conditionId)
        {
            var condition = MedicService.GetConditionForEdit(conditionId);
            condition.Studies
                .Remove(condition.Studies.Where(x => x.Id == studyId)
                .FirstOrDefault());

            WorkUnit.Commit();

            return Json("success");
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult RemoveUserStoryFromCondition(int userStoryId, int conditionId)
        {
            var condition = MedicService.GetConditionForEdit(conditionId);
            condition.UserStories
                .Remove(condition.UserStories.Where(x => x.Id == userStoryId)
                .FirstOrDefault());

            WorkUnit.Commit();

            return Json("success");
        }

        [YogaAuthorize]
        public ActionResult AddCondition()
        {
            return View(new NewConditionModel());
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult AddCondition(NewConditionModel model)
        {
            var condition = new Condition
            {
                Name = model.Name,
                Description = model.Description,
                MetaDescription = model.MetaDescription,
                Active = true
            };

            MedicService.AddCondition(condition);
            WorkUnit.Commit();

            return RedirectToAction("Update", new { Id = condition.Id });
        }

        [YogaAuthorize]
        public ActionResult DeactivatedConditions()
        {
            var model = new DeavtivatedConditionsModel();
            var deactivatedConditions = MedicService.GetDeactivatedConditions();
            var conditionModelList = new List<ConditionModel>();

            foreach (var condition in deactivatedConditions)
            {
                conditionModelList.Add(new ConditionModel(condition));
            }

            model.Conditions = conditionModelList;
            return View(model);
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult DeactivateCondition(int id)
        {
            var condition = MedicService.GetCondition(id);
            condition.Active = false;
            WorkUnit.Commit();

            return Json("success");
        }

        [YogaAuthorize]
        [HttpPost]
        public ActionResult ReactivateCondition(int conditionId)
        {
            var condition = MedicService.GetDeactivatedCondition(conditionId);
            condition.Active = true;
            WorkUnit.Commit();

            return Json("success");
        }
    }
}
