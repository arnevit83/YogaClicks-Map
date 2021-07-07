using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clicks.Yoga.Context;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Static;

namespace Clicks.Yoga.Portal.Controllers
{
    public class StaticController : YogaController
    {
        public StaticController(
            IWorkUnit workUnit, 
            ISecurityContext securityContext,
            ITeacherService teacherService)
            : base(workUnit, securityContext) 
        {
            TeacherService = teacherService;
        }

        private ITeacherService TeacherService { get; set; }

        [ActionName("advertise-with-us")]
        public ActionResult Advertise()
        {
            return View();
        }

        [ActionName("site-introduction")]
        public ActionResult SiteIntroduction()
        {
            return View();
        }

        [ActionName("site-map")]
        public ActionResult HowToUse()
        {
            return View("how-to-use-this-site");
        }

        [ActionName("profile-professional-guide")]
        public ActionResult ProfileProfessionalGuide(bool registered = false)
        {
            var teacher = TeacherService.GetTeacherForCurrentUser();

            var vm = new HowToUseModel
            {
                Registered = registered,
                HasTeacher = teacher != null ? true : false
            };

            return View(vm);
        }

        [ActionName("manifesto")]
        public ActionResult Manifesto()
        {
            return View();
        }

        [ActionName("meet-the-team")]
        public ActionResult MeetTheTeam()
        {
            return View();
        }

        [ActionName("our-story")]
        public ActionResult OurStory()
        {
            return View();
        }

        [ActionName("press-release")]
        public ActionResult PressRelease()
        {
            return View();
        }

        [ActionName("privacy-policy")]
        public ActionResult Privacy()
        {
            return View();
        }

        [ActionName("terms-of-use")]
        public ActionResult TermsOfUse()
        {
            return View();
        }

        [ActionName("beginners-faq")]
        public ActionResult BeginnersFaq()
        {
            return View();
        }

        [ActionName("history-of-yoga")]
        public ActionResult History()
        {
            return View();
        }

        [ActionName("is-yoga-a-religion")]
        public ActionResult IsYogaReligion()
        {
            return View();
        }

        [ActionName("what-is-yoga")]
        public ActionResult WhatIsYoga()
        {
            return View();
        }

        public ActionResult Community()
        {
            return View();
        }

        public ActionResult Practice()
        {
            return View();
        }

        public ActionResult Wisdom()
        {
            return View();
        }

        public ActionResult Discover()
        {
            return View();
        }
    }
}
