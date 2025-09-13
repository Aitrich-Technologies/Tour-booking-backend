namespace TourBooking.API.Participant.RequestObjects
{
    public class AddParticipantRequest
    {
        public Guid LeadId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PassportNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? PlaceOfBirth { get; set; }
    }
}
