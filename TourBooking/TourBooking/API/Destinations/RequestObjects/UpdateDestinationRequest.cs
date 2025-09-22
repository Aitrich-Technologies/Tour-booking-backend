using System.ComponentModel.DataAnnotations;

namespace TourBooking.API.Destination.RequestObjects
{
    public class UpdateDestinationRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;

    }
}
