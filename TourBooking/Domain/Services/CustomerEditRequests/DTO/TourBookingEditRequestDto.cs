using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CustomerEditRequests.DTO
{
    public class TourBookingEditRequestDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public TourBookingFormDto Booking { get; set; }
        public Guid RequestedByUserId { get; set; }
        public string? Reason { get; set; }
        public string Status { get; set; }   // <-- ENUM → STRING
        public DateTime RequestedAt { get; set; }
    }

}
