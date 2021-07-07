using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileQuestionsUpdateModel
    {
          public ProfileQuestionsUpdateModel()
        {
        }

          public ProfileQuestionsUpdateModel(Profile profile, IEnumerable<ProfileQuestion> questions, IEnumerable<ProfileAnswer> answers)
        {
            ProfileId = profile.Id;
            ProfileQuestions = new ProfileQuestionCaptureModel(questions, answers);
        }

        public int ProfileId { get; set; }

        public ProfileQuestionCaptureModel ProfileQuestions { get; set; }
       
    }
}