using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using System.ComponentModel.DataAnnotations;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.YogaMap
{
    public class YogaMap
    {


        public YogaMap(IEnumerable<Condition> conditions, IAccountService accountService, ISecurityContext securityContext) 
        {
            Conditions = new List<NamedSummaryModel>();
            foreach (var condition in conditions)
                Conditions.Add(new NamedSummaryModel(condition));
            FilererrorEmail = "";
            TS = new List<string>();
            TS.Add("Teacher");
            TS.Add("Shop");
            TS.Add("Student");
            if (securityContext.Authenticated)
            {
                UserHasConnectedFacebook = accountService.CurrentUserHasFederatedLogin("Facebook");
            }
            else
            {
                UserHasConnectedFacebook = false;
            }
        }


        public YogaMap()
        {
            FilererrorEmail = "Please try again";
            TS = new List<string>();
            TS.Add("Teacher");
            TS.Add("Shop");
            TS.Add("Student");
        }






        public YogaMap(YmStory Story, IEnumerable<Condition> conditions, IAccountService accountService, ISecurityContext securityContext)
        {
            Conditions = new List<NamedSummaryModel>();
            foreach (var condition in conditions)
                Conditions.Add(new NamedSummaryModel(condition));
            ViewModel = new YogaStoryModel(Story);
            FilererrorEmail = "";
            TS = new List<string>();
            TS.Add("Teacher");
            TS.Add("Student");


            if (securityContext.Authenticated)
            {
                UserHasConnectedFacebook = accountService.CurrentUserHasFederatedLogin("Facebook");
            }
            else
            {
                UserHasConnectedFacebook = false;
            }
        }
        public IList<NamedSummaryModel> Conditions { get; set; }


        public List<string> TS { get; set; }


        public string TSreturn { get; set; }


        public YogaStoryModel ViewModel { get; set; }


        public bool UserHasConnectedFacebook { get; set; }



        [Display(Name = "Email address")]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "The_email_address_is_required", ErrorMessage = null)]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "Invalid_Email_Address", ErrorMessage = null)]
        public string EmailAddress { get; set; }

        [Display(Name = "To Name")]
        [Required(ErrorMessage = "Please specify a to name")]
        public string ToName { get; set; }

        [Display(Name = "From Name")]
        [Required(ErrorMessage = "Please specify a from name")]
        public string FromName { get; set; }


        public string FilererrorEmail { get; set; }





        public AddStoryPopUpModelUpdate AddStoryupdateModel { get; set; }







    }

}