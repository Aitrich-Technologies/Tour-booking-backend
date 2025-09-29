using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace TourBooking.API.Notes.RequestObjects
{
    public class UpdateNoteRequest
    {

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid TourId { get; set; }

        [MaxLength(1000)]
        public string? TourNotes { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
