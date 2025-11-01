using Domain.Models;
using Domain.Services.Participant.DTO;
using Domain.Services.Tour.DTO;
using Domain.Services.Users.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TourBooking.DTO
{
    public class GetBookingDto
    {
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public Guid UserId { get; set; }
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

        public string? ParticipantType { get; set; }

        public string? Status { get; set; }

        public TourDto? Tour { get; set; }

        public UserResponseDto? User { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ParticipantDto>? Participants { get; set; }
    }
}
