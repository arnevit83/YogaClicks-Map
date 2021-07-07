using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class UserStoryModel : EntityModel<UserStory>
    {
        public UserStoryModel() { }

        public UserStoryModel(UserStory entity) : base(entity) { }

        public string Title { get; set; }

        public string Content { get; set; }

        public bool OwnerHidden { get; set; }

        public List<TeacherModel> Teachers { get; set; }

        public List<VenueModel> Venues { get; set; }

        public User Owner { get; set; }

        public override void Populate(UserStory entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Content = entity.Content;
            OwnerHidden = entity.OwnerHidden;
            Owner = entity.Owner;

            var teachers = new List<TeacherModel>();
            var venues = new List<VenueModel>();

            foreach (var teacher in entity.Teachers)
            {
                teachers.Add(new TeacherModel(teacher));
            }

            foreach (var venue in entity.Venues)
            {
                venues.Add(new VenueModel(venue));
            }

            Teachers = teachers;
            Venues = venues;
        }

    }
}