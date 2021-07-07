using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Preview.Models
{
    public class RegisterModel
    {
        [Required]
        public string EmailAddress { get; set; }

        public bool IsTeacher { get; set; }

        public bool IsVenue { get; set; }
    }
}