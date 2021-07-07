using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicks.Yoga.Domain.Entities
{
    // DEPLOYMENT OF YODAMEDS
    // add the searchable interfaece and edit each condition so that it populates in the search results.
    // public class Condition : Entity, ISearchable, IProfileBanner, INamed    

    public class Condition : PrincipalEntity, ISearchable, IProfileBanner, INamed, IActor
    {
        public string Name { get; set; }
        public Image Image { get; private set; }

        public string Description { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public virtual Image DirectoryImage { get; set; }

        public virtual Image ProfileBanner { get; set; }

        public string ImageCourtesyOf { get; set; }

        public virtual ICollection<Study> Studies { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

        public virtual ICollection<TeacherTrainingOrganisation> TeacherTrainingOrganisations { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        public virtual ICollection<WhatTheScienceSays> WhatTheScienceSayses { get; set; }

        public virtual ICollection<UserStory> UserStories { get; set; }

        public virtual ICollection<SearchRecord> SearchRecords { get; set; }

        public virtual ICollection<Follow> Followers { get; set; }

     
        public bool IsSearchable
        {
            get { return Active; }
        }
        string IEntityHandle.Location
        {
            get { return null; }
        }

        public void PopulateSearchRecord(SearchRecord record)
        {
            record.OwnerId = ((ISecurable)this).OwnerId;
            record.Name = Name;
            record.Description = Description;

            if (record.Description != null && record.Description.Length > 150)
                record.Description = record.Description.Substring(0, 150) + "...";


        }

   
        IEnumerable<ISearchable> ISearchable.GetChildSearchables()
        {
            yield break;
        }
    }
}
