using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Groups;
using Clicks.Yoga.Portal.Models.Shared;
using Microsoft.Web.Mvc;
using System.Configuration;
using System.Linq;
using Clicks.Yoga.Domain.Services.Interfaces;

namespace Clicks.Yoga.Portal.Controllers
{
    public class GroupsController : YogaController
    {
        public GroupsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IGroupService groupService,
            IEntityService entityService,
            IFriendService friendService,
            IImageService imageService,
            IProfileService profileService,
            IStyleService styleService,
            IMedicService medicService)
            : base(workUnit, securityContext)
        {
            GroupService = groupService;
            EntityService = entityService;
            FriendService = friendService;
            ImageService = imageService;
            ProfileService = profileService;
            StyleService = styleService;
            MedicService = medicService;
        }

        private IGroupService GroupService { get; set; }

        private IEntityService EntityService { get; set; }

        private IFriendService FriendService { get; set; }

        private IImageService ImageService { get; set; }

        private IProfileService ProfileService { get; set; }

        private IStyleService StyleService { get; set; }

        private IMedicService MedicService { get; set; }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        public ActionResult ProfessionalPartial(int entityId, string entityTypeName)
        {
            var professional = EntityService.GetEntity<IEntityHandle>(entityId, entityTypeName);

            if (professional == null)
                throw new UnknownEntityException();

            var promotedGroups = GroupService.GetProfessionalPromotedGroups(entityId, entityTypeName);
            var joinedGroups = GroupService.GetProfessionalJoinedGroups(entityId, entityTypeName);

            return View(new GroupProfessionalPartialModel(professional, promotedGroups, joinedGroups));
        }

        public ActionResult Display(int id)
        {
            var permitted = false;

            permitted = CheckIfPermitted(id);

            if (!permitted) return RedirectToAction("Groups", "Search");

            return RedirectToAction("Wall");
        }

        public ActionResult Profile(int id)
        {
            var permitted = CheckIfPermitted(id);
            if (!permitted) return RedirectToAction("Groups", "Search");

            var group = GroupService.GetGroupForDisplay(id);
            var permissions = GroupService.GetPermissions(id);

            return View(new GroupDisplayModel(group, permissions, SecurityContext.CurrentProfile));
        }

        public ActionResult Members(int id, int skip = 0, int take = -1)
        {
            var permitted = CheckIfPermitted(id);
            if (!permitted) return RedirectToAction("Groups", "Search");

            if (take == -1) take = int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.InfiniteScroll.DefaultNumberOfGroupMembers"]);
            var permissions = GroupService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var group = GroupService.GetGroupForDisplay(id);

            return View(new GroupMembersModel(group, permissions));
        }

        [HttpPost]
        public ActionResult GetMemberCount(int groupId)
        {
            var membersCount = GroupService.GetMembersCount(groupId);
            return Json(membersCount);
        }

        public ActionResult MembersPartial(int id,
                                           int? groupOwnerId,
                                           bool permissionsManage = false,
                                           bool permissionsmanageAdmin = false,
                                           int skip = 0,
                                           int take = -1)
        {
            if (take == -1) take = int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.InfiniteScroll.DefaultNumberOfGroupMembers"]);
            var members = GroupService.GetMembers(id, skip, take);
            return View(new GroupMembersPartialModel(members, groupOwnerId, permissionsManage, permissionsmanageAdmin));
        }

        public ActionResult Wall(int id)
        {
            var permitted = CheckIfPermitted(id);
            if (!permitted) return RedirectToAction("Groups", "Search");

            var permissions = GroupService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var group = GroupService.GetGroupForDisplay(id);

            return View(new GroupWallModel(group, permissions));
        }

        public ActionResult Galleries(int id)
        {
            var permitted = CheckIfPermitted(id);
            if (!permitted) return RedirectToAction("Groups", "Search");

            var permissions = GroupService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var group = GroupService.GetGroupForDisplay(id);

            return View(new GroupGalleriesModel(group, permissions));
        }

        public ActionResult Gallery(int id, int galleryId)
        {
            var permitted = CheckIfPermitted(id);
            if (!permitted) return RedirectToAction("Groups", "Search");

            var permissions = GroupService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var group = GroupService.GetGroupForDisplay(id);

            return View(new GroupGalleryModel(group, permissions, galleryId));
        }

        public ActionResult Videos(int id)
        {
            var permitted = CheckIfPermitted(id);
            if (!permitted) return RedirectToAction("Groups", "Search");

            var permissions = GroupService.GetPermissions(id);

            if (!permissions.Access)
                throw new PermissionDeniedException();

            var group = GroupService.GetGroupForDisplay(id);

            return View(new GroupVideosModel(group, permissions));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult InvitePartial(int id)
        {
            var friends = GroupService.GetInvitableFriends(id, SecurityContext.CurrentUser.Id);
            return View(new GroupInvitePartialModel(friends));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult MembershipButton(int id)
        {
            return View("MembershipButton", new GroupMembershipButtonModel(GroupService.GetMembershipStatus(id)));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Join(int id)
        {
            GroupService.Join(id);
            WorkUnit.Commit();
            return MembershipButton(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Unjoin(int id)
        {
            GroupService.Unjoin(id);
            WorkUnit.Commit();
            return MembershipButton(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult RequestInvitation(int id)
        {
            GroupService.RequestMembership(id);
            WorkUnit.Commit();
            return MembershipButton(id);
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult InviteFriends(int id, int[] friendIds)
        {
            if (friendIds == null) return new EmptyResult();
            GroupService.InviteFriends(id, friendIds);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult RemoveMembers(int id, int[] userIds)
        {
            if (userIds == null) return new EmptyResult();
            GroupService.RemoveMembers(id, userIds);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult PromoteMembers(int id, int[] userIds)
        {
            if (userIds == null) return new EmptyResult();
            GroupService.PromoteMembers(id, userIds);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult DemoteMembers(int id, int[] userIds)
        {
            if (userIds == null) return new EmptyResult();
            GroupService.DemoteMembers(id, userIds);
            WorkUnit.Commit();
            return new EmptyResult();
        }

        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var group = GroupService.GetGroupForEditing(id);
            var permissions = GroupService.GetPermissions(id);

            return View(new GroupUpdateModel(group).PopuplateMetadata(group, permissions, ProfileService, StyleService, MedicService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, GroupUpdateModel model)
        {
            var group = GroupService.GetGroupForEditing(id);

            model.PopulateEntity(group, SecurityContext, EntityService, ImageService, StyleService, MedicService);
            WorkUnit.Commit();

            return RedirectToAction("Display");
        }

        [YogaAuthorize]
        public ActionResult Settings(int id)
        {
            var group = GroupService.GetGroupForEditing(id);
            var permissions = GroupService.GetPermissions(id);

            return View(new GroupSettingsModel(group, permissions));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Settings(int id, GroupSettingsModel model)
        {
            var group = GroupService.GetGroupForEditing(id);

            model.PopulateEntity(group);
            WorkUnit.Commit();

            return RedirectToAction("Display");
        }

        [YogaAuthorize]
        public ActionResult Create(int ownerId)
        {
            var createModel = new GroupCreateModel();

            ViewBag.CreateModel = createModel;
            var step1 = new GroupCreateStep1Model(ownerId).PopulateMetadata(ProfileService);
            step1.Name = "dsfg";
            createModel.Step1Model = step1;
            var devar = (string)new MvcSerializer().Serialize(createModel, SerializationMode.Signed);


            var x = (GroupCreateModel)new MvcSerializer().Deserialize(devar, SerializationMode.Signed);
            return View("CreateStep1", new GroupCreateStep1Model(ownerId).PopulateMetadata(ProfileService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStep1(GroupCreateStep1Model model, string createModel)
        {
            var devar = (GroupCreateModel)new MvcSerializer().Deserialize(createModel, SerializationMode.Signed);
            devar.Step1Model = model;
            ViewBag.CreateModel2 = devar;
            if (!ModelState.IsValid) return View(model);
            return View("CreateStep2", new GroupCreateStep2Model().PopulateMetadata(ProfileService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStep2(GroupCreateStep2Model model, string createModel2, string createModel3)
        {
            var devar = new GroupCreateModel();

            if (String.IsNullOrEmpty(createModel2))
            {
                devar = (GroupCreateModel)new MvcSerializer().Deserialize(createModel3, SerializationMode.Signed);
                devar.Step2Model = model;
                ViewBag.CreateModel3 = devar;
            }
            else
            {
                devar = (GroupCreateModel)new MvcSerializer().Deserialize(createModel2, SerializationMode.Signed);
                devar.Step2Model = model;
                ViewBag.CreateModel3 = devar;
            }
        
            if (model.Back)
            {
                ModelState.Clear();
                ViewBag.CreateModel = devar;
                return View("CreateStep1", devar.Step1Model.PopulateMetadata(ProfileService));
            }

            if (!ModelState.IsValid) return View(model.PopulateMetadata(ProfileService));
            return View("CreateStep3", new GroupCreateStep3Model().PopulateMetadata(SecurityContext.CurrentProfile, FriendService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStep3(GroupCreateStep3Model model, string createModel3)
        {

            if (model.Back)
            {
                var devtemp = (GroupCreateModel)new MvcSerializer().Deserialize(createModel3, SerializationMode.Signed);
                ViewBag.createModel2 = devtemp;
                ModelState.Clear();
                return View("CreateStep2", devtemp.Step2Model);
            }

            var devar = (GroupCreateModel)new MvcSerializer().Deserialize(createModel3, SerializationMode.Signed);
            devar.Step3Model = model;

            if (!ModelState.IsValid) return View(model.PopulateMetadata(SecurityContext.CurrentProfile, FriendService));


            var group = new Group { Owner = SecurityContext.CurrentUser };
            devar.PopulateEntity(group, SecurityContext.CurrentProfile, SecurityContext, EntityService, FriendService);
            ViewBag.createModel3 = devar;

            GroupService.CreateGroup(group);
            WorkUnit.Commit();

            return View("CloseModal", new CloseModalModel(Url.Action(group.Id.ToString(), "Groups"), "CreateGroup"));
        }


        public ActionResult Faqs()
        {
            var group = GroupService.GetFaqGroup();

            if (group == null) return HttpNotFound();

            return RedirectToAction("Wall", new { id = group.Id });
        }

        #region Helpers
        private bool CheckIfPermitted(int id)
        {
            if (id == int.Parse(ConfigurationManager.AppSettings["Clicks.Yoga.NewsUpdateGroup.Id"]))
            {
                if (SecurityContext.Authenticated)
                {
                    var permittedUserIdStrings = ConfigurationManager.AppSettings["Clicks.Yoga.NewsUpdateGroup.PermitedUserIds"].Split(',');
                    var permittedUserIds = permittedUserIdStrings.Select(x => int.Parse(x)).ToList();
                    if (!permittedUserIds.Contains(SecurityContext.CurrentActor.Id))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}