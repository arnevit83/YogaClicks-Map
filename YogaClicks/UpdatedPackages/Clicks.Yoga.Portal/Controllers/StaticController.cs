using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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
                 IEmailService emailService,
               IProfileService profileService,
            ITeacherService teacherService)
            : base(workUnit, securityContext) 
        {
            TeacherService = teacherService;
            ProfileService = profileService;
            EmailService = emailService;
        }

        private ITeacherService TeacherService { get; set; }
        private IProfileService ProfileService { get; set; }
        private IEmailService EmailService { get; set; }
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

        [ActionName("sell-with-us")]
        public ActionResult sellwithus()
        {
            return View();
        }

        //     [ActionName("work-with-us")]
        //public ActionResult  workwithus()
        //{
        //    return View();
        //}
    
        [ActionName("YCAmbassador")]
        public ActionResult YCAmbassador()
        {
            return View();
        }
                [ActionName("yogamap")]
        public ActionResult yogamap()
        {
            return View();
        }



       [HttpPost]
       public ActionResult UsEmail(string email, string name, string message )
       {

           if (email == "")
           {
               return Json("fail");
           }

           try
           {
               EmailService.SendEmail("YCwebsite@yogaclicks.com", "Message from " + name, email + "<br /><br />" + message);


           }
           catch (Exception)
           {

               return Json("fail");
           }
        

           return Json("success");
       }


        [ActionName("site-map")]
        public ActionResult HowToUse()
        {
            return View("how-to-use-this-site");
        }

        [ActionName("Lucys-story")]
        public ActionResult Lucysstory()
        {
            return View("Lucys-story");
        }

            [ActionName("PoweredByYoga")]
        public ActionResult PoweredByYoga()
        {
            return View("PoweredByYoga");
        }
        

        [ActionName("profile-professional-guide")]
        public ActionResult ProfileProfessionalGuide(bool registered = false)
        {

            var teacher = TeacherService.GetTeacherForCurrentUser();

            var vm = new HowToUseModel
            {
                Registered = registered,
                HasTeacher = false
                //HasTeacher = teacher != null ? true : false
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
            return View("Lucys-story");
        }

        [ActionName("Explore")]
        public ActionResult Explore()
        {
            if (SecurityContext.Authenticated)
            {
                
                var profile = ProfileService.GetProfileForEditing(SecurityContext.CurrentProfile.Id);
            if (profile.IsFirstVisit)
            {
                profile.IsFirstVisit = false;
                WorkUnit.Commit();
        
            }  
         
            }
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

        [ActionName("Quiz")]
        public ActionResult Quiz()
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

        public ActionResult faq()
        {
            return View();
        }
  
        public ActionResult Discover()
        {
            return View();
        }
        public ActionResult Trending()
        {
            return View();
        }
    }
}
