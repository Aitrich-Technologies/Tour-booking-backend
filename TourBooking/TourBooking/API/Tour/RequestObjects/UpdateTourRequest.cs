using Domain.Enums;

namespace TourBooking.API.Tour.RequestObjects
{
    public class UpdateTourRequest
    {
        public Guid Id { get; set; }
        public string TourName { get; set; } = string.Empty;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public int? NoOfNights { get; set; }
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ArrivalDate { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid ConsultantId { get; set; }
        public TourStatus Status { get; set; }
    }
}
