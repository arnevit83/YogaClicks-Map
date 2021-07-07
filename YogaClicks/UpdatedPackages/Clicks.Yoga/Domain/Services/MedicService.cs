using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Services
{
    public class MedicService : ServiceBase, IMedicService
    {
        public MedicService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) { }

        public Author GetAuthor(int id)
        {
            return EntityContext.Authors
                .Where(x => x.Active == true)
                .FirstOrDefault();
        }

        public List<Author> GetAuthorsForStudy(int studyId)
        {
            return EntityContext.Authors
                .Where(x => x.Studies.FirstOrDefault().Id == studyId)
                .ToList();
        }

        public Condition GetCondition(int id)
        {
            return EntityContext.Conditions
                .Where(x => x.Id == id)
                .Where(x => x.Active == true)
                .FirstOrDefault();
        }

        public Condition GetConditionForEdit(int id)
        {
            return EntityContext.Conditions
                .Include(x => x.ProfileBanner)
                .Include(x => x.Studies)
                .Include(x => x.UserStories)
                .Where(x => x.Id == id)
                .Where(x => x.Active == true)
                .FirstOrDefault();
        }

        public void AddCondition(Condition condition)
        {
            EntityContext.Conditions.Add(condition);
        }

        public List<Condition> GetDeactivatedConditions()
        {
            return EntityContext.Conditions
                .Where(x => x.Active == false)
                .ToList();
        }

        public Condition GetDeactivatedCondition(int id)
        {
            return EntityContext.Conditions
               .Where(x => x.Id == id)
               .FirstOrDefault();
        }

        public List<Condition> GetConditions()
        {
            return EntityContext.Conditions
                .Where(x => x.Active == true)
                .ToList();
        }

        public Condition GetConditionWithStudies(int id)
        {
            return EntityContext.Conditions
                .Include(x => x.Studies.Where(y => y.Active == true))
                .Where(x => x.Active == true)
                .Where(x => x.Id == id)
                .FirstOrDefault(); ;
        }

        public List<Condition> GetConditionsForInfiniteScroll(int skip, int take)
        {
            return EntityContext.Conditions
              .Where(x => x.Active == true)
              .OrderBy(x => x.Name)
              .Skip(skip)
              .Take(take)
              .ToList();
        }

        public Condition GetEntireCondition(int id)
        {
            return EntityContext.Conditions
                .Include(x => x.ProfileBanner)
                .Include(x => x.Studies.Where(y => y.Active == true))
                .Include(x => x.Teachers.Where(y => y.Active == true))
                .Include(x => x.TeacherTrainingOrganisations.Where(y => y.Active == true))
                .Include(x => x.Venues.Where(y => y.Active == true))
                .Where(x => x.Id == id)
                .Where(x => x.Active == true)
                .FirstOrDefault();
        }

        public Study GetStudy(int id)
        {
            return EntityContext.Studies
                .Where(x => x.Active == true)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public Study GetStudyForEdit(int id)
        {
            return EntityContext.Studies
                .Include(x => x.Authors)
                .Include(x => x.TherapyTypes)
                .Where(x => x.Active == true)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<Study> GetStudiesForCondition(int conditionId)
        {
            return EntityContext.Studies
                .Include(x => x.TherapyTypes)
                .Include(x => x.Authors)
                .Where(x => x.Conditions.Select(y => y.Id).Contains(conditionId))
                .Where(x => x.Active == true)
                .ToList();
        }

        public TherapyType GetTherapyType(int id)
        {
            return EntityContext.TherapyTypes
                .Where(x => x.Active == true)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public TherapyType GetTherapyTypeByName(string name)
        {
            return EntityContext.TherapyTypes
                    .Where(x => x.Active == true)
                    .Where(x => x.Name == name)
                    .FirstOrDefault();
        }

        public List<TherapyType> GetTherapyTypes()
        {
            return EntityContext.TherapyTypes
                    .Where(x => x.Active == true)
                    .ToList();
        }

        public List<TherapyType> GetTherapyTypesByNames(List<string> names)
        {
            return EntityContext.TherapyTypes
                       .Where(x => x.Active == true)
                       .Where(x => names.Contains(x.Name))
                       .ToList();
        }

        public UserStory GetUserStory(int id)
        {
            return EntityContext.UserStories
                .Where(x => x.Active == true)
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public void AddUserStory(UserStory userStory)
        {
            EntityContext.UserStories.Add(userStory);
        }

        public List<UserStory> GetUserStoriesForCondition(int conditionId)
        {
            return EntityContext.UserStories
                .Include(x => x.Owner)
                .Include(x => x.Owner.Profile)
                .Include(x => x.Owner.Profile.Image)
                .Include(x => x.Teachers)
                .Include(x => x.Venues)
                .Where(x => x.Conditions.Select(y => y.Id).Contains(conditionId))
                .Where(x => x.Active == true)
                .ToList();
        }

        public WhatTheScienceSays GetWhatTheScienceSays(int id)
        {
            return EntityContext.WhatTheScienceSayses
                .Where(x => x.Id == id)
                .Where(x => x.Active == true)
                .FirstOrDefault();
        }

        public List<WhatTheScienceSays> GetWhatTheScienceSaysForCondition(int conditionId)
        {
            return EntityContext.WhatTheScienceSayses
                .Include(x => x.TherapyTypes)
                .Where(x => x.Conditions.Select(y => y.Id).Contains(conditionId))
                .Where(x => x.Active == true)
                .ToList();
        }

        public void AddStudy(Study study)
        {
            EntityContext.Studies.Add(study);
        }
    }
}
