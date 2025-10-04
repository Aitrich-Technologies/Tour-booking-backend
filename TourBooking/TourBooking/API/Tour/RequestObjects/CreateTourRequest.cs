using Domain.Enums;
using System.Text.Json.Serialization;

namespace TourBooking.API.Tour.RequestObjects
{
    public class CreateTourRequest
    {
        public string TourName { get; set; } = string.Empty;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public int? NoOfNights { get; set; }
        public int Price { get; set; }
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ArrivalDate { get; set; }
        public Guid? CustomerId { get; set; }
        [JsonIgnore]
        public Guid ConsultantId { get; set; }
        public string Status { get; set; }
    }
}
