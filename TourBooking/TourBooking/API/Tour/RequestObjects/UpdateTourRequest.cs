namespace TourBooking.API.Tour.RequestObjects
{
    namespace TourBooking.Services.Tours.DTO
    {
        public class UpdateTourDto
        {
            public string? TourName { get; set; }
            public string? TourDescription { get; set; }
            public Guid? DestinationId { get; set; }
            public int? NoOfNights { get; set; }
            public decimal? Price { get; set; }            // ✅ nullable
            public DateTime? DepartureDate { get; set; }   // ✅ nullable
            public DateTime? ArrivalDate { get; set; }     // ✅ nullable
            public Guid? CustomerId { get; set; }          // ✅ nullable
            public Guid? ConsultantId { get; set; }        // ✅ nullable
            public string? Status { get; set; }            // ✅ string, no .Value
        }
    }
}

   

