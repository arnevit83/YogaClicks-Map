using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Helpers;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Stories
{
    public class MapStoriesModel
    {

        public MapStoriesModel(IEnumerable<YmStory> stories,ISecurityContext securityContext)
        {
            Stories = new List<YogaMapStory>();
            foreach (var story in stories)
            {
                Stories.Add(new YogaMapStory(story,securityContext));
            }
        }


        public IList<YogaMapStory> Stories { get; private set; }

    }
        public class YogaMapStory
{
            public YogaMapStory(YmStory story,ISecurityContext securityContext)
            {

                SmallDesc = story.Story.Length > 155 ? story.Story.Substring(0, 155) + " ..." : story.Story;

                Video = "";
                Name = story.Name != "" ? story.Name : story.Owner.Profile.Name;
                if (story.Image != null)
                {
                    Name = story.Name;
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

                typexy = story.ProfileType ?? "Student";



                var baseUri = new Uri(ConfigurationManager.AppSettings["Clicks.Yoga.ImageStore.Url"]);
                if (story.Image != null)
                {
                    Image = baseUri + "images/yogaimages/" + story.Image.Uri +
                            "?width=668&height=668&scale=both&mode=crop";
                }
                else if (story.Owner.Profile.Image != null)
                {
                    Image = baseUri + "images/yogaimages/" + story.Owner.Profile.Image.Uri +
                            "?width=668&height=668&scale=both&mode=crop";
                }
                else
                {

                    switch (typexy)
                    {
                        case "Student":
                        {
                            Image = "/images/svgicons/Icon_ProfileBlue.svg";
                            break;
                        }   
                        case "Shop":
                        {
                            Image = "/images/svgicons/shopicon.svg";
                            break;
                        }   
                              case "Teacher":
                        {
                            Image = "/images/svgicons/Icon_TeacherPurple.svg";
                            break;
                        }   
                    }


                  
                }




                PBYSince = story.PoweredFrom.Year.ToString();



                if (story.VideoUpload != null)
                {
                    var c = new VideoModel(story.VideoUpload);
                    Video = c.GetEmbedHtml(643, 401);
                }

                if (securityContext.Authenticated)
                {

                    if (story.Owner.Profile.Id == securityContext.CurrentProfile.Id)
                    {
                        Auth = "<br/><a class='Editbuttonspop'  onclick='Editbuttonfunk(" + story.Id +
                               ")' href='javascript:void(0);' id='Editbutton' data-toggle='tooltip' title='Edit your story'><i class='fa  fa-pencil-square-o'></i></a><a class='Deletebuttonspop'  onclick='Removebuttonfunk(" +
                               story.Id +
                               ")' href='javascript:void(0);' id='Removebutton' data-toggle='tooltip' title='Delete your story'><i class='fa  fa-trash'></i></a>";


                    }
                    else
                    {
                        if (story.Address != null)
                        {
                            if (story.Address.Area != null)
                            {
                                if (story.Address.Area.Length > 20)
                                {
                                    Auth = "<span class='popuppbydate'>" + story.Address.Area.Substring(0, 19) +
                                           "...</span>";
                                }
                                else
                                {
                                    Auth = "<span class='popuppbydate'>" + story.Address.Area + "</span>";
                                }

                            }
                            if (story.Address.City != null)
                            {
                                if (story.Address.City.Length > 20)
                                {
                                    Auth = "<span class='popuppbydate'>" + story.Address.City.Substring(0, 19) +
                                           "...</span>";
                                }
                                else
                                {
                                    Auth = "<span class='popuppbydate'>" + story.Address.City + "</span>";
                                }

                            }
                        }

                    }





                }
                else
                {
                    if (story.Address != null)
                    {
                        if (story.Address.Area != null)
                        {
                            if (story.Address.Area.Length > 20)
                            {
                                Auth = "<span class='popuppbydate'>" + story.Address.Area.Substring(0, 19) +
                                       "...</span>";
                            }
                            else
                            {
                                Auth = "<span class='popuppbydate'>" + story.Address.Area + "</span>";
                            }

                        }
                        if (story.Address.City != null)
                        {
                            if (story.Address.City.Length > 20)
                            {
                                Auth = "<span class='popuppbydate'>" + story.Address.City.Substring(0, 19) +
                                       "...</span>";
                            }
                            else
                            {
                                Auth = "<span class='popuppbydate'>" + story.Address.City + "</span>";
                            }

                        }
                    }

                }



                if (story.VideoUpload != null)
                {
                    Auth +=
                        "<a class='Videobuttonspop'  onclick='Videobuttonfunk()' href='javascript:void(0);' id='Videobutton' data-toggle='tooltip' title='Watch their video story'><i class='fa fa-play-circle'></i></a>";


                }
                else
                {
                    Auth += "&nbsp;";
                }
               




            }
            public string Name { get; set; }
            public int Id { get; set; }
            public float Lat { get; set; }
            public float Lng { get; set; }
            public string Image { get; set; }
            public string typexy { get; set; }
            public string Auth { get; set; }
            public string SmallDesc { get; set; }
            public string PBYSince { get; set; }

            public string Video { get; set; }

}


       

}