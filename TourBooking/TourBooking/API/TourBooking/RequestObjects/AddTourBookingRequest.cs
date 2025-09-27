using Domain.Enums;

namespace TourBooking.API.TourBooking.RequestObjects
{
    public class AddTourBookingRequest
    {
            public Guid TourId { get; set; }        
            public string FirstName { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public string? Gender { get; set; }
            public DateOnly? Dob { get; set; }
            public string? Citizenship { get; set; }
            public string? PassportNumber { get; set; }
            public DateOnly? IssueDate { get; set; }
            public DateOnly? ExpiryDate { get; set; }
            public string? PlaceOfBirth { get; set; }
            public bool? LeadPassenger { get; set; }
            public ParticipantType ParticipantType { get; set; }
            public TourStatus Status { get; set; }
        }
    }


