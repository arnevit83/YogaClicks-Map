using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Services.Interfaces
{
    public interface IMedicService
    {
        Author GetAuthor(int id);
        List<Author> GetAuthorsForStudy(int studyId);
        Condition GetCondition(int id);
        Condition GetConditionForEdit(int id);
        void AddCondition(Condition condition);
        List<Condition> GetDeactivatedConditions();
        Condition GetDeactivatedCondition(int id);
        List<Condition> GetConditions();
        Condition GetConditionWithStudies(int id);
        List<Condition> GetConditionsForInfiniteScroll(int skip, int take);
        Condition GetEntireCondition(int id);
        Study GetStudy(int id);
        Study GetStudyForEdit(int id);
        List<Study> GetStudiesForCondition(int conditionId);
        TherapyType GetTherapyType(int id);
        List<TherapyType> GetTherapyTypes();
        List<TherapyType> GetTherapyTypesByNames(List<string> names);
        TherapyType GetTherapyTypeByName(string name);
        UserStory GetUserStory(int id);
        void AddUserStory(UserStory userStory);
        List<UserStory> GetUserStoriesForCondition(int conditionId);
        WhatTheScienceSays GetWhatTheScienceSays(int id);
        List<WhatTheScienceSays> GetWhatTheScienceSaysForCondition(int conditionId);
        void AddStudy(Study study);
    }
}