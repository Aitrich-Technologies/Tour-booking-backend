using System;
using Domain.Enum;

namespace TourBooking.Services.Tours.DTO
{
    public class TourRegisterDto
    {
        public string TourName { get; set; } = null!;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public int? NoOfNights { get; set; }
        public int Price { get; set; }
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ArrivalDate { get; set; }


        // Foreign keys for AuthUser
        public Guid? CustomerId { get; set; }    // nullable as you wanted
        public Guid ConsultantId { get; set; }

        public TourStatus Status { get; set; }

        // Reference existing DB records by ID
        //public List<Guid> TermsAndConditionIds { get; set; } = new();
        //public List<Guid> NoteIds { get; set; } = new();
    }
    }






