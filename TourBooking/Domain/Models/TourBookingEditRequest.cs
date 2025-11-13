using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TourBookingEditRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BookingId { get; set; }
        public TourBookingForm Booking { get; set; } = null!;
        public Guid RequestedByUserId { get; set; }
        public string? Reason { get; set; }
        public EditStatus Status { get; set; } = EditStatus.Pending; // Pending, Approved, Rejected
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RespondedAt { get; set; }
    }

}
