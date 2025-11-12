using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ParticipantEditRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ParticipantId { get; set; }
        public Guid RequestedBy { get; set; } // Customer who requested
        public string? UpdatedDataJson { get; set; } // Store updated fields temporarily as JSON
        public string Status { get; set; } = "PENDING"; // PENDING, APPROVED, REJECTED
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAt { get; set; }

        // Navigation property
        public ParticipantInformation? Participant { get; set; }
    }
}
