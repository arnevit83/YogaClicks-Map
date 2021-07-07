using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Profiles;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    [DataContract]
    public class ProfileStep5ViewModel
    {
        public ProfileStep5ViewModel()
        {
        }

        public ProfileStep5ViewModel(IEnumerable<ProfileQuestion> questions, IEnumerable<ProfileAnswer> answers)
        {
            ProfileQuestions = new ProfileQuestionCaptureModel(questions, answers);
        }

        public ProfileQuestionCaptureModel ProfileQuestions { get; set; }

    }
}