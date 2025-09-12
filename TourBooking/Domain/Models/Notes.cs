using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public partial class Notes
    {
        public Guid Id { get; set; }

        public Guid TourId { get; set; }

        public string? TourNotes { get; set; }

        public NotesStatus Status { get; set; }

        public virtual Tour Tour { get; set; } = null!;
    }
}
