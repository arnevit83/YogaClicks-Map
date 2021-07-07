using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using System.ComponentModel.DataAnnotations;

namespace Clicks.Yoga.Portal.Models.YogaMap
{
    public class MapLogin
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string Destination { get; set; }
        public string ValidationMap { get; set; }
        public bool Persist { get; set; }




    }

}