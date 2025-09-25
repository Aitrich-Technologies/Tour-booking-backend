using AutoMapper;
using Domain.Services.TourBooking.DTO;
using TourBooking.API.TourBooking.RequestObjects;

namespace TourBooking.API.TourBooking
{
    public class TourBookingProfile:Profile
    {
        public TourBookingProfile()
        {
            CreateMap<AddTourBookingRequest, TourBookingDto>();
            CreateMap<TourBookingDto, AddTourBookingRequest>();
        }
    }
}
