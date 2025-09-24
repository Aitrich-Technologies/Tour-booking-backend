using System.ComponentModel.DataAnnotations;

namespace TourBooking.API.Destination.RequestObjects
{
    public class CreateDestinationRequest
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;



    }
}
