using Domain.Enum;
using System.Text.Json.Serialization;

namespace TourBooking.API.Tour.RequestObjects
{
    public class AddTourRequest
    {


        public string TourName { get; set; } = null!;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public int? NoOfNights { get; set; }
        public int Price { get; set; }
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ArrivalDate { get; set; }


        public Guid? CustomerId { get; set; }
        public Guid ConsultantId { get; set; }
        public TourStatus Status { get; set; }



    }
}





