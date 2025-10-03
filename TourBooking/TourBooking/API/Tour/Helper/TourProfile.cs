using AutoMapper;
using Domain.Enums;
using Domain.Services.Tour.DTO;
using Domain.Services.User.DTO;
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
            // RequestDto -> Entity (string to enum)
            CreateMap<TourDto, Tourss>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Status, true)));

            // Entity -> ResponseDto (enum to string)
            CreateMap<Tourss, TourDto>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status.ToString()))
         
        .ForMember(dest => dest.DestinationName,
                   opt => opt.MapFrom(src => src.Destination.Name)) // ✅ flatten Destination.Name
         .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Destination.ImageUrl))
        .ForMember(dest => dest.Status,
                   opt => opt.MapFrom(src => src.Status.ToString()));// enum → string


            //CreateMap<TourDto, Tourss>().ReverseMap();
        }
    }
}
