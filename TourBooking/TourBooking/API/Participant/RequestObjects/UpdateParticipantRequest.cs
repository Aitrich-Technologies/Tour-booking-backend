namespace TourBooking.API.Participant.RequestObjects
{
    public class UpdateParticipantRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Citizenship { get; set; }
        public string? PassportNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? PlaceOfBirth { get; set; }
    }
}
