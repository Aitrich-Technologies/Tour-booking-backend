using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services.Tour.DTO;
using TourBooking.API.Tour.RequestObjects;

namespace TourBooking.API.Tour.Helper
{
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            // Request → DTO
            CreateMap<CreateTourRequest, TourDto>();
            CreateMap<UpdateTourRequest, TourDto>();
            CreateMap<UpdateTourStatusRequest, UpdateTourStatusDto>();
            //// Tourss -> TourDto mapping
            //CreateMap<Tourss, TourDto>()
            //    .ForMember(dest => dest.DestinationName,
            //               opt => opt.MapFrom(src => src.Destination != null ? src.Destination.Name : null))
            //    .ForMember(dest => dest.ImageUrl,
            //               opt => opt.MapFrom(src => src.Destination != null ? src.Destination.ImageUrl : null))
            //    .ForMember(dest => dest.Status,
            //               opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<Tourss, TourDto>()
    .ForMember(dest => dest.DestinationName,
               opt => opt.MapFrom(src => src.Destination != null ? src.Destination.Name : null))
    .ForMember(dest => dest.ImageUrl,
               opt => opt.MapFrom(src => src.Destination != null ? src.Destination.ImageUrl : null))
    .ForMember(dest => dest.Status,
               opt => opt.MapFrom(src => src.Status.ToString()))
    .ReverseMap()   // <-- this adds TourDto -> Tourss
    .ForMember(dest => dest.Destination, opt => opt.Ignore()) // avoid circular mapping
    .ForMember(dest => dest.Status,
               opt => opt.MapFrom(src => Enum.Parse<TourStatus>(src.Status, true)));

        }
    }
}
