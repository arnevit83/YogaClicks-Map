using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchResultModel
    {
        public SearchResultModel() {}

        public SearchResultModel(SearchResult result)
        {
            Id = result.EntityId;
            Type = result.EntityTypeName;
            TypePlural = result.EntityTypeDisplayPlural;
            Controller = result.Controller;
            Name = result.Name;
            Description = result.Description;
            City = result.City;
            Area = result.Area;
            Country = result.Country;
            Postcode = result.Postcode;
            Image = new ImageModel(result.ImagePath, result.ImageGuid, result.ImageExtension);

            if (result.Location != null)
            {
                Latitude = result.Location.Latitude;
                Longitude = result.Location.Longitude;
            }

            Rating = result.Rating;
            FriendshipStatus = result.FriendshipStatus;
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public string TypePlural { get; set; }

        public string Controller { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Country { get; set; }

        public string Postcode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public ImageModel Image { get; set; }

        public decimal? Rating { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }

        public string Uri
        {
            get { return string.Format("/{0}/{1}", Controller, Id); }
        }

        public string Address
        {
            get
            {
                return string.Join(", ", new List<string> { City, Area, Postcode, Country }
                    .Where(s => !string.IsNullOrEmpty(s)).ToArray());
            }
        }
    }
}