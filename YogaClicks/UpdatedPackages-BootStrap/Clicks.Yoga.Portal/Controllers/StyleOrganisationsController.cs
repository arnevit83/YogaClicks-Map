using System;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Models.Shared;
using Clicks.Yoga.Portal.Models.StyleOrganisations;
using Microsoft.Web.Mvc;
using Clicks.Yoga.Portal.Enums.Profiles;

namespace Clicks.Yoga.Portal.Controllers
{


    //Obsolete Removed/not in use
    #region 

    public class StyleOrganisationsController : YogaController
    {
        public StyleOrganisationsController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
            IImageService imageService,
            IStyleService styleService,
            IStyleOrganisationService styleOrganisationService,
            IWebsiteService websiteService)
            : base(workUnit, securityContext)
        {
            ImageService = imageService;
            StyleService = styleService;
            StyleOrganisationService = styleOrganisationService;
            WebsiteService = websiteService;
        }

        private IImageService ImageService { get; set; }

        private IStyleService StyleService { get; set; }

        private IStyleOrganisationService StyleOrganisationService { get; set; }

        private IWebsiteService WebsiteService { get; set; }

        public ActionResult Navigation()
        {
            var organisations = StyleOrganisationService.GetOrganisations();
            var styles = organisations.Select(o => o.Style).Distinct();

            return View(new OrganisationNavigationModel(organisations, styles));
        }


        public ActionResult Display(int id)
        {
            var organisation = StyleOrganisationService.GetOrganisationForDisplay(id);

            ChangeActorIfOwner(organisation);

            return View(new OrganisationDisplayModel(organisation));
        }


        [YogaAuthorize]
        public ActionResult Fans(int id)
        {
            var organisation = StyleOrganisationService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View("Fans", new OrganisationFansModel(organisation));
        }


        [YogaAuthorize]
        public ActionResult Activities(int id)
        {
            var organisation = StyleOrganisationService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationActivitiesModel(organisation));
        }


        [YogaAuthorize]
        public ActionResult Groups(int id)
        {
            var organisation = StyleOrganisationService.GetOrganisationForDisplay(id);

            foreach (var result in EnsureUrlSlug(organisation)) return result;

            ChangeActorIfOwner(organisation);

            return View(new OrganisationGroupsModel(organisation));
        }


        [YogaAuthorize]
        public ActionResult Create(int ownerId)
        {
            var createModel = new OrganisationCreateModel();
            ViewBag.CreateModel = createModel;
            return View("CreateStep1", new OrganisationCreateStep1Model(ownerId).PopulateMetadata(StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStep1(OrganisationCreateStep1Model model,
            [Deserialize] OrganisationCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;
            if (!ModelState.IsValid) return View(model.PopulateMetadata(StyleService));
            createModel.Step1Model = model;
            return View("CreateStep2", new OrganisationCreateStep2Model());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStep2(OrganisationCreateStep2Model model,
            [Deserialize] OrganisationCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateStep1", createModel.Step1Model.PopulateMetadata(StyleService));
            }

            if (!ModelState.IsValid) return View(model);
            createModel.Step2Model = model;
            return View("CreateStep3", new OrganisationCreateStep3Model());
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult CreateStep3(OrganisationCreateStep3Model model,
            [Deserialize] OrganisationCreateModel createModel)
        {
            ViewBag.CreateModel = createModel;

            if (model.Back)
            {
                ModelState.Clear();
                return View("CreateStep2", createModel.Step2Model);
            }

            if (!ModelState.IsValid) return View(model);
            createModel.Step3Model = model;

            var organisation = StyleOrganisationService.CreateOrgansiation(createModel.Step1Model.OwnerId);
            createModel.PopulateEntity(organisation, StyleService, WebsiteService);
            WorkUnit.Commit();

            var wizard = WizardStatus.None;

            if (Session["WizardStatus"] != null)
                wizard = (WizardStatus) Session["WizardStatus"];

            return RedirectToAction("CongratulationsProfileCreated", "Profiles",
                new {id = organisation.Id, profileType = ProfileTypeEnum.SO});
        }


        [YogaAuthorize]
        public ActionResult Update(int id)
        {
            var organisation = StyleOrganisationService.GetOrganisationForEditing(id);

            return View(new OrganisationUpdateModel(organisation).PopulateMetadata(organisation, StyleService));
        }

        [HttpPost]
        [YogaAuthorize]
        public ActionResult Update(int id, OrganisationUpdateModel model)
        {
            var organisation = StyleOrganisationService.GetOrganisationForEditing(id);

            if (!ModelState.IsValid) return View(model.PopulateMetadata(organisation, StyleService));

            model.PopulateEntity(organisation, ImageService, StyleService, WebsiteService);
            organisation.ModificationTime = DateTime.Now;
            WorkUnit.Commit();

            return RedirectToAction("Display");
        }
    }

    #endregion


}