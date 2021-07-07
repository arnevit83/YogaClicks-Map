using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using System.Collections;
using Clicks.Yoga.Context;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Medic;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.YogaMap
{
    public class AddStoryPopUpModelUpdate
    {
        public AddStoryPopUpModelUpdate()
        {
            Displaymodel = new AddStoryPopUpModel();
 
            Conditions = new SignUpConditionChooserModel();

            Challenge = new LifeChallengeChooserModel();
        
        }

        public AddStoryPopUpModelUpdate(YmStory story, IMedicService medicService, IAddressService addressService, ITeacherService teacherService, IVenueService venueService, IYmStoryService storyLifeChallengeService)
        {
            TSreturn = "";

            Displaymodel = new AddStoryPopUpModel(story, medicService,addressService, teacherService, venueService,  storyLifeChallengeService);

            Image = new StoryImageEditorModel(story.Image);

          

   
           
        }
        public void PopulateMetadata(IMedicService medicService, IAddressService addressService, IAccountService accountService, ITeacherService teacherService, IVenueService venueService, IYmStoryService storyLifeChallengeService)
        {
       //Displaymodel.Conditions.PopulateMetadata(medicService.GetConditions());
           var TS = new List<string>();
           TS.Add("Teacher");
           TS.Add("Student");
           Displaymodel.TS = TS;

        
           var YearTaught = new List<int>();
           YearTaught = Enumerable.Range(1930, int.Parse(DateTime.Now.ToString("yyyy")) - 1930 + 1)
                   .OrderByDescending(x => x)
                   .ToList();
           Displaymodel.YearTaught = YearTaught;
           Displaymodel = new AddStoryPopUpModel(new YmStory(), medicService, addressService, teacherService, venueService, storyLifeChallengeService);
            Image = new StoryImageEditorModel();
          //  UserHasConnectedFacebook = accountService.CurrentUserHasFederatedLogin("Facebook");
        }



        public void PopulateEntity(YmStory entity, Profile profile, IMedicService medicService, IImageService imageService, IAddressService addressService, ITeacherService teacherService, IVenueService venueService, IYmStoryService storyLifeChallengeService)
        {
            //Image to be done
         //   entity.Image = Image.PopulateEntity(() => Image.Image, imageService);
           // entity.Condition = medicService.GetCondition(Conditionsreturn);
            //Displaymodel.Conditions = new SignUpConditionChooserModel(medicService.GetConditions);
            //e Conditions = new List<ConditionModel>();ntity.Condition = Displaymodel.Conditions.PopulateMetadata(medicService.GetConditions());
            if (Conditions.Ids.Count > 0)
            {
                entity.Conditions = new List<Condition>();
            }
            
            var idList = Conditions.Ids;
            if (entity.Conditions != null)
            {
                 entity.Conditions.Clear();
            }
           
            foreach (var Id in idList)
            {
                entity.Conditions.Add(medicService.GetCondition(Id));
            }











            if (Challenge.Ids.Count > 0)
            {
                entity.LifeChallenges = new List<YmStoryLifeChallenge>();
            }

            var idListC = Challenge.Ids;
            if (entity.LifeChallenges != null)
            {
                entity.LifeChallenges.Clear();
            }

            foreach (var Id in idListC)
            {
                entity.LifeChallenges.Add(storyLifeChallengeService.GetLifeChallenge(Id));
            }



            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
              entity.Teachers = new List<Teacher>();
              entity.Venues = new List<Venue>();

            if (Displaymodel.Teachers != null)
            {
                var includedTeachers = teacherService.GetTeachers(Displaymodel.Teachers.Ids.ToList());
                entity.Teachers.Clear();
                foreach (var teacher in includedTeachers)
                {
                    entity.Teachers.Add(teacher);
                }
            }

            if (Displaymodel.Venues != null)
            {
                var includedTeachers = venueService.GetVenues(Displaymodel.Venues.Ids.ToList());
                entity.Venues.Clear();
                foreach (var venues in includedTeachers)
                {
                    entity.Venues.Add(venues);
                }
            }




            entity.Name = profile.Forename + " " +  profile.Surname;

            entity.Owner = profile.Owner;
            entity.PoweredFrom = new DateTime(YearTaughtreturn,01,01);
            entity.ProfileType = TSreturn;

            if (Displaymodel.Address.Latitude == 0)
            {


              //  entity.Address = newStory.Address;

            }
            else
            {
                entity.Address = Displaymodel.Address.PopulateEntity(entity.Address, addressService);
                entity.Lat = float.Parse(Displaymodel.Address.Latitude.ToString());
                entity.Lng = float.Parse(Displaymodel.Address.Longitude.ToString());
            }

            
   
            entity.Title = Displaymodel.Title ?? "Story coming soon.";
            entity.Story = Displaymodel.Story ?? "...";
         
        }

        //here you would pass in model that fails validation..
        public AddStoryPopUpModel Displaymodel { get; set; }

        public SignUpConditionChooserModel Conditions { get; set; }

        public LifeChallengeChooserModel Challenge { get; set; }
        

        [Range(1930, 3000, ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_a_required_field")]
        public int YearTaughtreturn { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_a_required_field")]
        public String TSreturn { get; set; }


        public int Fbimage { get; set; }
        public String FbLocation { get; set; }
        public StoryImageEditorModel Image { get; set; }


  
      
    }


}
