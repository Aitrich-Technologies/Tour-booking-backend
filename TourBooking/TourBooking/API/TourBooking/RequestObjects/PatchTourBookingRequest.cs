using Domain.Enums;

namespace TourBooking.API.TourBooking.RequestObjects
{
    public class PatchTourBookingRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; }

        public string? Citizenship { get; set; }

        public bool? LeadPassenger { get; set; }


        public string? PlaceOfBirth { get; set; }


        public ParticipantType ParticipantType { get; set; }

    }
}
