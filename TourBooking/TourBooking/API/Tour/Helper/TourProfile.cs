using AutoMapper;
using Domain.Services.Tour.DTO;
using TourBooking.API.Tour.RequestObjects;

namespace TourBooking.API.Tour.Helper
{
    public class TourProfile:Profile
    {
        public TourProfile()
        {
            CreateMap<CreateTourRequest, TourDto>();
            CreateMap<UpdateTourRequest, TourDto>();
            CreateMap<UpdateTourStatusRequest, UpdateTourStatusDto>();
        }
    }
}
