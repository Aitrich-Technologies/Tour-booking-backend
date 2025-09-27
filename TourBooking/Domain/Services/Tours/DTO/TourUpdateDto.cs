using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Tours.DTO
{
    public class UpdateTourDto
    {
        public string? TourName { get; set; }
        public string? TourDescription { get; set; }
        public Guid? DestinationId { get; set; }
        public int? NoOfNights { get; set; }
        public int? Price { get; set; }   // match entity type
        public DateOnly? DepartureDate { get; set; }  // match entity type
        public DateOnly? ArrivalDate { get; set; }    // match entity type
        public Guid? CustomerId { get; set; }
        public Guid? ConsultantId { get; set; }
        public TourStatus? Status { get; set; }
    }


}

