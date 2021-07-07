

using System;
using System.Collections.Generic;
using System.Configuration;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using Clicks.Yoga.Portal.Models.Medic;

namespace Clicks.Yoga.Portal.Models.YogaMap
{
    public class AssociatedPartialProfileModel
    {


        public AssociatedPartialProfileModel(IEnumerable<YmStory> stories, IEnumerable<Condition> conditions)
         {
            
            Conditions = new List<NamedSummaryModel>();
            foreach (var condition in conditions)
                Conditions.Add(new NamedSummaryModel(condition));
            if (stories != null) {
            Stories = new List<AssociatedPartialProfileModelMini>();
            foreach (var story in stories)
                Stories.Add(new AssociatedPartialProfileModelMini(story));
 }


            Header = "I'm #PoweredByYoga - are you?";


         }

     



        public IList<AssociatedPartialProfileModelMini> Stories { get; private set; }

        public IList<NamedSummaryModel> Conditions { get; set; }

        public string Header { get; set; }


    }

    public class AssociatedPartialProfileModelMini
    {


        public AssociatedPartialProfileModelMini(YmStory story)
        {
            if (story == null)
            {
                return;
            }
            StoryId = story.Id;


         
            var baseUri = new Uri(ConfigurationManager.AppSettings["Clicks.Yoga.ImageStore.Url"]);
          



            if (story.Image != null)
            {
                Image = baseUri + "images/yogaimages/" + story.Image.Uri;
            }
            else if (story.Owner.Profile.Image != null)
            {
                Image = baseUri + "images/yogaimages/" + story.Owner.Profile.Image.Uri;
            }
            else
            {
                Image = "/images/svgicons/Icon_ProfileGreen.svg ";
            }






            PoweredFrom = story.PoweredFrom.Year.ToString();
            Conditions = new List<ConditionModel>();
            foreach (Condition condition in story.Conditions)
            {
                Conditions.Add(new ConditionModel(condition));
            }

         

            if (story.Title != null)
            {
                if (story.Title.Length > 100)
                {
                    Title = story.Title.Substring(0, 96) + " ...";
                }
                else
                {
                    Title = story.Title;
                }

            }
            else
            {
                Title = "";

            }


            if (story.Story != null)
            {
                if (story.Story.Length > 100)
                {
                    Story = story.Story.Substring(0, 96) + " ...";
                }
                else
                {
                    Story = story.Story;
                }

            }
            else
            {
                Story = "";

            }
        }

     
       public IList<ConditionModel> Conditions { get; set; }
        public string PoweredFrom { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }


        public int StoryId { get; set; }

        public String Image { get; set; }

    }


}