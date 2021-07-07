
using System;
using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class YmStory : PrincipalEntity, IFanable
    {



        public string Name { get; set; }

   
        public virtual Image Image { get; set; }

        public virtual Video VideoUpload { get; set; }
        public string Location { get; private set; }

        public virtual ICollection<Condition> Conditions { get; set; }

        public virtual ICollection<YmStoryLifeChallenge> LifeChallenges { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
 


        public DateTime PoweredFrom { get; set; }
        public string ProfileType { get; set; }
        public string Title { get; set; }
        public string Story { get; set; }
        public string Video { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }

        public string ShopUrl { get; set; }

        public virtual Address Address { get; set; }

    }
}