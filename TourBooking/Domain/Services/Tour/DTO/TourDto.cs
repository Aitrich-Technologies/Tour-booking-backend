using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tour.DTO
{
    public class TourDto
    {
        public Guid Id { get; set; }
        public string TourName { get; set; } = string.Empty;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public int? NoOfNights { get; set; }
        public int Price { get; set; }
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ArrivalDate { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid ConsultantId { get; set; }
        public TourStatus Status { get; set; } 
    }
}
