namespace TourBooking.API.Destinations.RequestObjects
{
    public class PatchDestinationRequest
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
