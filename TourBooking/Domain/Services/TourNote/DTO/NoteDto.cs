using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services.TourNote.Interface;

namespace Domain.Services.TourNote.DTO
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public Guid TourId { get; set; }
        public string? TourNotes { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
