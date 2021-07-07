
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Spatial;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Services
{
    public class YmStoryService : ServiceBase, IYmStoryService
    {
        public YmStoryService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}


        public IList<YmStoryLifeChallenge> GetLifeChallenges()
        {
            return EntityContext.YmStoryLifeChallenges.Select(x => x).OrderByDescending(x => x.CreationTime).ToList();
        }
        public YmStoryLifeChallenge GetLifeChallenge(int id)
        {
            return EntityContext.YmStoryLifeChallenges.Where(x => x.Id == id).FirstOrDefault(); ;
        }

           public IList<YmStory> GetMapStoriesBucket(int bucket)
           {

               //     var klash = EntityContext.YmStory.Join(EntityContext.YmStoryLifeChallenges ,c => c.Id, l => l.Id,(c,l)=> new
               // {
               //     OrderID = l.LifeChallengebucket..,
                    
               // }).ToList(); 

               //return EntityContext.YmStory.Where(x => x.Conditions.Count > 0).OrderByDescending(x => x.ModificationTime).ToList(); 
         

            return  EntityContext.YmStory.Where(x => x.LifeChallenges.Any(y => y.LifeChallengebucket.Any(a => a.Id == bucket) )).ToList();
         
        }

        public YmStory GetStory(int id)
        {
            return EntityContext.YmStory.Get(e => e.Id == id);
        }


        public IList<YmStory> GetMapStoriesConditions()
        {
            return EntityContext.YmStory.Where(x => x.Conditions.Count > 0).OrderByDescending(x => x.ModificationTime).ToList(); ;
        }

    

        
        public IList<YmStory> GetStories()
        {
            return EntityContext.YmStory.Select(x => x).OrderByDescending(x => x.CreationTime).ToList();
        }





        public void AddStory(YmStory story)
        {
            EntityContext.YmStory.Add(story);
        }
        public void RemoveStory(YmStory story)
        {
            EntityContext.YmStory.Remove(story);
        }
   

        public IList<YmStory> SearchStories(String TS,int? conditionId)
        {
            if (TS == "" && conditionId != null)
            {
                //return EntityContext.YmStory.Where(x => x.Condition.Id == conditionId).ToList();
                return EntityContext.YmStory.Where(x => x.Conditions.Any(y => y.Id == conditionId)).OrderByDescending(x => x.CreationTime).ToList();
            }
            else
            {

                if (conditionId == null)
                {
                    return EntityContext.YmStory.Where(x => x.ProfileType == TS).OrderByDescending(x => x.CreationTime).ToList();
                }
                else if (TS == "")
                {
                    return EntityContext.YmStory.Where(x => x.Conditions.Any(y => y.Id == conditionId)).OrderByDescending(x => x.CreationTime).ToList();
                }
                else
                {
                    return EntityContext.YmStory.Where(x => x.Conditions.Any(y => y.Id == conditionId)).Where(x => x.ProfileType == TS).OrderByDescending(x => x.CreationTime).ToList();
                }

                
            }


          
        }

        public IList<YmStory> GetStories(int ownerId, string TS)
        {
            if (TS != "" && ownerId != 0)
            {
                return EntityContext.YmStory.Where(x => x.Owner.Id == ownerId).Where(x => x.ProfileType == TS).OrderByDescending(x => x.CreationTime).ToList();

            }

            return null;

        }
        public IList<YmStory> GetStoriesFav(int ownerId, string TS, string proType)
        {

            if (TS != "" && ownerId != 0 && proType != "")
            {
                if (proType == "TeacherStudent")
                {
                    return EntityContext.YmStory.Where(x => x.Teachers.Any(y => y.Id == ownerId)).OrderByDescending(x => x.CreationTime).ToList();
                }
                else
                {
                    return EntityContext.YmStory.Where(x => x.Venues.Any(y => y.Id == ownerId)).Where(x => x.ProfileType == TS).OrderByDescending(x => x.CreationTime).ToList();
                }
            }
            return null;

        }

        public bool HasStory(int profileId )
        {
      
            if  (EntityContext.YmStory.Any(x => x.Owner.Id == profileId))
            {
                return true;
            }
             return false;
           
            
        }
  

    }
}