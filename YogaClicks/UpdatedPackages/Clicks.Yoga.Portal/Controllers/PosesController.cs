﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Galleries;
using Clicks.Yoga.Portal.Models.Poses;
using Clicks.Yoga.Portal.Models.Galleries;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Controllers
{
    public class PosesController : YogaController
    {
        public PosesController(
            IWorkUnit workUnit,
            ISecurityContext securityContext,
                IImageService imageService,
              IPoseUserImageService poseImageService,
            IPoseService poseService,
               IMedicService medicService
        ) : base(workUnit, securityContext)
        {
            PoseService = poseService;
            PoseImageService = poseImageService;
            ImageService = imageService;
            MedicService = medicService;
        }

        private IPoseUserImageService PoseImageService { get; set; }
        private IPoseService PoseService { get; set; }
        private IImageService ImageService { get; set; }
        private IMedicService MedicService { get; set; }
        public ActionResult Navigation(int? poseId, PoseNavigationType? type)
        {
            if (!type.HasValue) type = Session["Poses.Navigation"] as PoseNavigationType? ?? PoseNavigationType.Directory;
            
            var pose = poseId != null ? PoseService.GetPose(poseId.Value) : null;
            var sanskritEnabled = Session["Poses.SanskritEnabled"] as bool? ?? false;

            switch (type)
            {
                case PoseNavigationType.Directory:
                    return View("IndexNavigation", new PoseIndexModel(PoseService.GetPoseCategories(), PoseService.GetPoses(), pose));
                case PoseNavigationType.Finder:
                    return View("IndexNavigation", new PoseIndexModel(PoseService.GetPoseCategories(), PoseService.GetPoses(), pose));
                default:
                    return new EmptyResult();
            }
        }

        [HttpPost]
        public ActionResult Sanskrit(bool enabled)
        {
            Session["Poses.SanskritEnabled"] = enabled;
            return new EmptyResult();
        }

      
        public ActionResult Index(string search, int? abilityLevelId, SortOrder? order)
        {
            var poses = PoseService.GetPoses();
            var categories = PoseService.GetPoseCategories();

            if (!string.IsNullOrEmpty(search))
                poses = FilterPoses(poses, search);

            if (abilityLevelId.HasValue)
                poses = poses.Where(e => e.AbilityLevel.Id == abilityLevelId.Value).ToList();
            
            if (order.HasValue)
                poses = SortPoses(poses, order.Value);

            Session["Poses.Navigation"] = PoseNavigationType.Finder;
            Session["Poses.Last"] = Request.Url.ToString();

            return View(new PoseIndexModel(categories, poses));
        }

        
        public ActionResult Directory()
        {

          //  Response.Redirect("/");

            Session["Poses.Navigation"] = PoseNavigationType.Directory;
            Session["Poses.Last"] = Request.Url.ToString();

            return View(new PoseDirectoryModel(PoseService.GetPoses()));
        }

   
        public ActionResult Category(int id, string search, SortOrder? order)
        {
            IEnumerable<PoseCategory> categories = PoseService.GetPoseCategories();

            var poses = new List<Pose>();
            var cat = categories.FirstOrDefault(c => c.Id == id);

            if (cat == null)
                return RedirectToAction("Index", new {search, order});

            poses.AddRange(cat.Poses);

            if (!string.IsNullOrEmpty(search))
                poses = FilterPoses(poses, search);

            if (order.HasValue)
                poses = SortPoses(poses, order.Value);

            return View("Index", new PoseIndexModel(categories, poses, id));
        }

        
        public ActionResult Display(int id)
        {
            var pose = PoseService.GetPoseForDisplay(id);

            foreach (var result in EnsureUrlSlug(pose)) return result;

            var categories = PoseService.GetPoseCategories();
            var poses = new List<Pose>();

            var userImages = PoseImageService.GetPoseUserImage(id);

            var condition = MedicService.GetConditions();

            string[] lines = pose.Indications.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (var i in lines)
            {
                var s = condition.Find(xx => xx.Name == i.ToString());
                if (s != null)
                {
                    var action = Url.Action("Display", "Medic", new { id = s.Id, slug = s.Name });
                    if (s.Name == "Headache/Migraine") { action = action.Replace(s.Name, "Headache-Migraine"); }
                   


                    if (action != null)
                        pose.Indications = pose.Indications.Replace(i, "<a href=" + action + ">" + s.Name + "</a>");
                }
            }

            foreach (var cat in categories)
                poses.AddRange(cat.Poses);

            poses = SortPoses(poses, SortOrder.EnglishName);

            return View(new PoseDisplayModel(pose, categories, poses, userImages.OrderByDescending(x => x.Id).ToList(), condition));
        }

        private List<Pose> FilterPoses(IList<Pose> poses, string substring)
        {
            substring = substring.ToLower();
            return poses.Where(e => e.EnglishName.ToLower().Contains(substring) || e.SanskritName.ToLower().Contains(substring)).ToList();
        }


        [YogaAuthorize]
        public ActionResult DeleteImage(int Idss)
        {

            var profile = SecurityContext.CurrentProfile;



            var poseImage = PoseImageService.GetPoseImage(Idss);
            var PoseID = poseImage.Pose.Id;
            if (profile.Owner.Id != poseImage.Image.Owner.Id)
            {

                return RedirectToAction("Display", PoseID);

            }

            PoseImageService.RemoveImage(poseImage);
            WorkUnit.Commit();

            return RedirectToAction("Display", PoseID);

        }




        [YogaAuthorize]
        [HttpPost]
        public ActionResult UpdateDirectoryImage(UpdateUserPoseImagesUpdateModel model)
        {

            //try
            //{
             var Pose = PoseService.GetPose(model.PosesId);
            var poseImage = new PoseUserImages { Pose = Pose };
            model.PopulateEntity(poseImage, ImageService);

            PoseImageService.AddImage(poseImage);

            Pose.ModificationTime = DateTime.Now;

                WorkUnit.Commit();

                return RedirectToAction("Display", Pose.Id);

            //return RedirectToAction("Display", Pose.Id);
            //}
            //catch (Exception)
            //{
            //    return RedirectToAction("index");
            
            //}
         
        }

        private List<Pose> SortPoses(IList<Pose> poses, SortOrder order)
        {
            Func<Pose, string> selector;

            switch (order)
            {
                case SortOrder.EnglishName:
                    selector = e => e.EnglishName;
                    break;
                case SortOrder.SanskritName:
                    selector = e => e.SanskritName;
                    break;
                default:
                    selector = e => e.EnglishName;
                    break;
            }

            return poses.OrderBy(selector).ToList();
        }

        public enum  SortOrder
        {
            EnglishName,
            SanskritName
        }
    }

     
}

