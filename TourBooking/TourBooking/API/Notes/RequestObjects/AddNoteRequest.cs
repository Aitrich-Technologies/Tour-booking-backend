using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace TourBooking.API.Notes.RequestObjects
{
    public class AddNoteRequest
    {
        [Required]
        public Guid TourId { get; set; }

        [MaxLength(1000)]
        public string? TourNotes { get; set; }

        [Required]
        public NotesStatus Status { get; set; }
    }
}
