using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Humanizer;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class VenueTest
    {
        public SignUpVenueSelectorCreatorModel Venue { get; private set; }

        public VenueTest()
        {
            Venue = new SignUpVenueSelectorCreatorModel();
        }

        public VenueTest(Teacher teacher)
        {

            //todo populate venue
        }

        public void PopulateEntity(Teacher teacher, IWebsiteService websiteService, IImageService imageService, ITeacherService teacherService)
        {


            //todo get venue from posted data

        }
    }
}