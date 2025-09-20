using Domain.Enum;



namespace Domain.Services.Tours.DTO
{
    public class TourPutDto
    {
        public string TourName { get; set; } = null!;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public int? NoOfNights { get; set; }
        public int Price { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid ConsultantId { get; set; }
        public TourStatus Status { get; set; }
    }
}
