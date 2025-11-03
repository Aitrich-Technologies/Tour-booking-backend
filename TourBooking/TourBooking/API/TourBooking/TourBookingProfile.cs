using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services.TourBooking.DTO;
using Domain.Services.Users.DTO;
using TourBooking.API.TourBooking.RequestObjects;

namespace TourBooking.API.TourBooking
{
    public class TourBookingProfile : Profile
    {
        public TourBookingProfile()
        {
            CreateMap<AddTourBookingRequest, TourBookingDto>();
            CreateMap<TourBookingDto, AddTourBookingRequest>();
            // RequestDto -> Entity (string to enum)
            CreateMap<TourBookingDto, TourBookingForm>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => Enum.Parse<TourStatus>(src.Status, true)))
                           .ForMember(dest => dest.ParticipantType,
                           opt => opt.MapFrom(src => Enum.Parse<ParticipantType>(src.ParticipantType, true)));

            // Entity -> ResponseDto (enum to string)
            CreateMap<TourBookingForm, TourBookingDto>()

                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status.ToString()))
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ParticipantType,
                           opt => opt.MapFrom(src => src.ParticipantType.ToString()));


            // Entity -> ResponseDto (enum to string)
            CreateMap<TourBookingForm, GetBookingDto>()

                        .ForMember(dest => dest.Status,
                                   opt => opt.MapFrom(src => src.Status.ToString()))
                        .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))

                        .ForMember(dest => dest.Participants,
                       opt => opt.MapFrom(src => src.ParticipantInformations))
                    .ForMember(dest => dest.ParticipantType,
                                   opt => opt.MapFrom(src => src.ParticipantType.ToString()));
        }
    }
}

