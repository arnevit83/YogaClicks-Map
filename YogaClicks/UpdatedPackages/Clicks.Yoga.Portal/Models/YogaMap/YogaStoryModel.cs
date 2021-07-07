using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using Clicks.Yoga.Portal.Models.Medic;

namespace Clicks.Yoga.Portal.Models.YogaMap
{
    public class YogaStoryModel :  EntityModel<YmStory> 
    {
        public IList<ConditionModel> Conditions { get; private set; }

        public YogaStoryModel(YmStory entity) : base(entity)
        {


          
        }
    

               
        public TeacherModel Teacher { get; set; }







        public string Name { get; set; }

    

        public float Lat { get; set; }
        public float Lng { get; set; }
        public string Image { get; set; }
        public string Caption { get; set; }
        public string PoweredFrom { get; set; }
        public string Type { get; set; }
        public string Story { get; set; }

        public int OwnerId { get; set; }
        public string Title { get; set; }
        public string Video { get; set; }

        public string ShopUrl { get; set; }

        public VideoModel VideoUpload { get; private set; }

        public override void Populate(YmStory story)
        {
           


            if (story == null)
            {
                return;
            }



            Conditions = new List<ConditionModel>();

            if (story.Conditions != null)
            {
                foreach (Condition condition in story.Conditions)
                {
                    Conditions.Add(new ConditionModel(condition));
                }
            }










            OwnerId = story.Owner.Id;

            var baseUri = new Uri(ConfigurationManager.AppSettings["Clicks.Yoga.ImageStore.Url"]);
            Name = story.Image != null ? story.Name : story.Owner.Profile.Name;
            if (story.Image != null)
            {
                Name =  story.Name;
            }
            else if (story.Owner.Profile.Name != null)
            {
                Name = story.Owner.Profile.Name;
            }
            else
            {
                Name = "";
            }
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            Name = myTI.ToTitleCase(Name);



            Id = story.Id;
            Lat = story.Lat;
            Lng = story.Lng;





            if (story.Image != null)
            {
                Image = baseUri + "images/yogaimages/" + story.Image.Uri ;
            }
            else if (story.Owner.Profile.Image != null)
            {
                Image = baseUri + "images/yogaimages/" + story.Owner.Profile.Image.Uri ;
            }
            else
            {
                Image = "/images/svgicons/Icon_ProfileGreen.svg ";
            }





            if (story.Title != null)
            {
                if (story.Title.Length > 100)
                {
                    Caption = story.Title.Substring(0, 96) + " ...";
                }
                else
                {
                    Caption = story.Title;
                }

            }
            else
            {
                Caption = "";

            }

            if (story.Video != null)
            {
                Video = story.Video;

            }

            if (story.VideoUpload != null)
            {
                VideoUpload = new VideoModel(story.VideoUpload);
            }
            if (story.ShopUrl != null)
            {
                ShopUrl = story.ShopUrl;
            }
            


            if (story.Title != null)
            {
                Title = story.Title;

            }
            if (story.Story != null)
            {
                Story = story.Story;

            }
            Type = story.ProfileType;




            PoweredFrom = story.PoweredFrom.Year.ToString();
        }


    }

}