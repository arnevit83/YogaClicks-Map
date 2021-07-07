namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherCreatePlacementOwnershipModel
    {
        public TeacherCreatePlacementOwnershipModel() {}

        public TeacherCreatePlacementOwnershipModel(int? venueId, string venueName)
        {
            VenueId = venueId;
            VenueName = venueName;
        }

        public int? VenueId { get; set; }

        public string VenueName { get; set; }

        public bool Owned { get; set; }

        public string EmailAddress { get; set; }
    }
}