namespace TourBooking.API.Participant.RequestObjects
{
    public class PatchParticipantRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PassportNumber { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string? PlaceOfBirth { get; set; }
    }

}
