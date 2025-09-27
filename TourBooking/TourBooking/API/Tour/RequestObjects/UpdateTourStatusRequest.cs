using Domain.Enums;

namespace TourBooking.API.Tour.RequestObjects
{
    public class UpdateTourStatusRequest
    {
        public TourStatus Status { get; set; }
    }
}
