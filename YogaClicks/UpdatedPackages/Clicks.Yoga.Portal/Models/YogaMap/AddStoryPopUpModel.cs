using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using System.Collections;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.YogaMap
{
    public class AddStoryPopUpModel
    {
        public AddStoryPopUpModel()
        {

            TS = new List<string>();
            Address = new YogaMapAddressEditorModel();
            VideoUrl = "";
            TS.Add("Teacher");
            TS.Add("Student");
            Lanpop = "NaN";
            Latpop = "NaN";
            Conditions = new SignUpConditionChooserModel();
            LifeChallenges = new LifeChallengeChooserModel();
            YearTaught = new List<int>();
            YearTaught = Enumerable.Range(1930, int.Parse(DateTime.Now.ToString("yyyy")) - 1930 + 1)
                    .OrderByDescending(x => x)
                    .ToList();
            Teachers = new TeacherChooserModel();
            Venues = new VenueChooserModel();


        }

        public AddStoryPopUpModel(YmStory story, IMedicService medicService, IAddressService addressService, ITeacherService teacherService, IVenueService venueService, IYmStoryService ymStoryLifeChallengeService)
        {
            VideoUrl = "";

            TS = new List<string>();
            TS.Add("Teacher");
            TS.Add("Student");
            Lanpop = "NaN";
            Latpop = "NaN";
       
            YearTaught = new List<int>();
            YearTaught = Enumerable.Range(1930, int.Parse(DateTime.Now.ToString("yyyy")) - 1930 + 1)
                .OrderByDescending(x => x)
                .ToList();

            var st = story.Conditions ?? new List<Condition>();
            var sg = story.LifeChallenges ?? new List<YmStoryLifeChallenge>();
            LifeChallenges = new LifeChallengeChooserModel(sg, ymStoryLifeChallengeService.GetLifeChallenges());
            LifeChallenges.PopulateMetadata(ymStoryLifeChallengeService.GetLifeChallenges());
            
            if (story.Address != null)
            {
                story.Address = Address.PopulateEntity(story.Address, addressService);
            }
            else
            {
                Address = new YogaMapAddressEditorModel();
            }

            Conditions = new SignUpConditionChooserModel(st, medicService.GetConditions());
            Conditions.PopulateMetadata(medicService.GetConditions());

            if (story.Teachers != null)
            {
                Teachers.PopulateCollection(story.Teachers, teacherService);
            }
            else
            {
                Teachers = new TeacherChooserModel();
            }

            if (story.Venues  != null)
            {
               Venues.PopulateCollection(story.Venues, venueService);
            }
            else
            {
                Venues = new VenueChooserModel();
            }

          
       


        }


        public SignUpConditionChooserModel Conditions { get; set; }

        public LifeChallengeChooserModel LifeChallenges { get; set; } 
       
        public List<int> YearTaught { get; set; }


    
        public List<string> TS { get; set; }


        public YogaMapAddressEditorModel Address { get; set; }

        public Boolean HasProflile { get; set; }

        public int IsupdateId { get; set; }

         [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_a_required_field")]
        public String Forename { get; set; }
         [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_a_required_field")]
        public String Surname { get; set; }

         public String Name { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "The_email_address_is_required", ErrorMessage = null)]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "Invalid_Email_Address", ErrorMessage = null)]
        public String Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_a_required_field")]
        public String Password { get; set; }

        public String Title { get; set; }

        public String Story { get; set; }

        public String Latpop { get; set; }

        public String Lanpop { get; set; }

        public String VideoUrl { get; set; }

        public TeacherChooserModel Teachers { get; set; }

        public VenueChooserModel Venues { get; set; }
      
    }


}
