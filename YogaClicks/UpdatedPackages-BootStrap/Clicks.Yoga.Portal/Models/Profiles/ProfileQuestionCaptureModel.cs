using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileQuestionCaptureModel
    {
        public ProfileQuestionCaptureModel() { }

        public ProfileQuestionCaptureModel(IEnumerable<ProfileQuestion> questions, IEnumerable<ProfileAnswer> answers)
        {
            Questions = new List<ProfileQuestionModel>();

            foreach (var q in questions)
                Questions.Add(new ProfileQuestionModel(q));

            foreach (var a in answers)
            {
                var model = Questions.FirstOrDefault(q => q.Id == a.QuestionId);

                if (model == null)
                    continue;

                model.Response = a.Text;
                model.AnswerId = a.Id;
            }
        }

        public IList<ProfileQuestionModel> Questions { get; set; }

        public IList<ProfileAnswer> CreateAnswers(Profile profile, IEnumerable<ProfileQuestion> questions)
        {
            return Questions.Where(q => !string.IsNullOrEmpty(q.Response)).Select(model => new ProfileAnswer {
                Profile = profile, ProfileId = profile.Id, QuestionId = model.Id, Question = questions.FirstOrDefault(q => q.Id == model.Id), Text = model.Response, Id = model.AnswerId.HasValue ? model.AnswerId.Value : 0
            }).ToList();
        }
    }
}