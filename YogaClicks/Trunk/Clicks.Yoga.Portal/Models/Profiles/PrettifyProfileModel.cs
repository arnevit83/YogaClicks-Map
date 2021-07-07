using Clicks.Yoga.Portal.Enums.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class PrettifyProfileModel
    {
        public int Id { get; set; }
        public ProfileTypeEnum ProfileType { get; set; }
        public string ProfileLink { get; set; }
    }
}