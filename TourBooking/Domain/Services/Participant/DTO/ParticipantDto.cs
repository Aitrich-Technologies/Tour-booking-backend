using Domain.Services.TourBooking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Participant.DTO
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public Guid LeadId { get; set; }
        public Guid BookingId { get; set; }
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
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
