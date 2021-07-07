using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using Clicks.Yoga.Geography;
using Nest;
using Newtonsoft.Json;

namespace Clicks.Yoga.Domain.Entities
{
    public class SearchRecord : Entity
    {
        public SearchRecord()
        {
            Styles = new List<Style>();
            Teachers = new List<Teacher>();
            TeacherServices = new List<TeacherService>();
            Venues = new List<Venue>();
            VenueServices = new List<VenueService>();
            AccreditationBodies = new LinkedList<AccreditationBody>();
            Conditions = new List<Condition>();
            LinkedEntities = new List<SearchLink>();
        }

        public SearchRecord(ISearchable subject)
            : this()
        {
            EntityId = subject.Id;
            subject.PopulateSearchRecord(this);
        }

        public virtual EntityType EntityType { get; set; }

        public int EntityId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        [ElasticProperty(Type = FieldType.geo_point)]
        public GeographicPoint Location { get; set; }

        public DateTime? Date { get; set; }

        [JsonIgnore]
        public DbGeography LocationGeography
        {
            get { return DbGeographyFunctions.PointToGeography(Location); }
            set { Location = DbGeographyFunctions.GeographyToPoint(value); }
        }

        public string StylesText
        {
            get { return string.Join(" ", Styles.Select(e => e.Name)); }
            private set { }
        }

        [JsonIgnore]
        public virtual ICollection<Style> Styles { get; private set; }

        [JsonIgnore]
        public virtual ICollection<Condition> Conditions { get; private set; }

        [JsonIgnore]
        public virtual ICollection<Teacher> Teachers { get; private set; }

        [JsonIgnore]
        public virtual ICollection<TeacherService> TeacherServices { get; private set; }

        [JsonIgnore]
        public virtual ICollection<Venue> Venues { get; private set; }

        [JsonIgnore]
        public virtual VenueType VenueType { get; set; }

        [JsonIgnore]
        public virtual ICollection<VenueService> VenueServices { get; private set; }

        [JsonIgnore]
        public virtual ICollection<AccreditationBody> AccreditationBodies { get; private set; }

        [JsonIgnore]
        public virtual Image Image { get; set; }

        public Guid? ImageGuid { get; set; }

        public string ImagePath { get; set; }

        public string ImageExtension { get; set; }

        public decimal? Rating { get; set; }

        public bool Stubbed { get; set; }

        public bool IsAccreditedTherapist { get; set; } 

        public int? OwnerId { get; set; }

        public virtual SearchRecord Parent { get; set; }

        public virtual ICollection<SearchLink> LinkedEntities { get; private set; }

        public void UpdateEntities(IEnumerable<IEnumerable<INamed>> Arrays)
        {
            LinkedEntities.Clear();

            foreach (var array in Arrays)
            {
                foreach (var item in array)
                {
                    LinkedEntities.Add(new SearchLink { Id = item.Id, Name = item.Name, TypeName = item.GetEntityTypeName() });
                }
            }
        }
    }
}