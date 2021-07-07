using System;
using System.Data.Spatial;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Entities
{
    public class SearchResult
    {
        public int TotalCount { get; set; }

        public int EntityId { get; set; }

        public string EntityTypeName { get; set; }

        public string EntityTypeDisplayName { get; set; }

        public string EntityTypeDisplayPlural { get; set; }

        public string Controller { get; set; }

        public string Name { get; set; }

        public double Distance { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public GeographicPoint Location { get; set; }

        public DbGeography LocationGeography
        {
            get { return DbGeographyFunctions.PointToGeography(Location); }
            set { Location = DbGeographyFunctions.GeographyToPoint(value); }
        }

        public Guid? ImageGuid { get; set; }

        public string ImagePath { get; set; }

        public string ImageExtension { get; set; }

        public decimal? Rating { get; set; }

        public bool Stubbed { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }

        public int? ParentEntityId { get; set; }

        public string ParentEntityTypeName { get; set; }

        public string ParentName { get; set; }

        public string ParentDescription { get; set; }

        public string ParentCity { get; set; }

        public string ParentArea { get; set; }

        public string ParentCountry { get; set; }

        public string ParentPostcode { get; set; }

        public GeographicPoint ParentLocation { get; set; }

        public DbGeography ParentLocationGeography
        {
            get { return DbGeographyFunctions.PointToGeography(ParentLocation); }
            set { ParentLocation = DbGeographyFunctions.GeographyToPoint(value); }
        }

        public Guid? ParentImageGuid { get; set; }

        public string ParentImagePath { get; set; }

        public string ParentImageExtension { get; set; }

        public decimal? ParentRating { get; set; }

        public bool IsAccreditedTherapist { get; set; }
    }
}