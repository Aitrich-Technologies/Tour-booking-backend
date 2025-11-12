using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services.Participant.DTO;
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

            CreateMap<TourBookingDto, TourBookingForm>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<BookStatus>(src.Status, true)))
                .ForMember(dest => dest.ParticipantType, opt => opt.MapFrom(src => Enum.Parse<ParticipantType>(src.ParticipantType, true)));

            CreateMap<TourBookingForm, TourBookingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.ParticipantType, opt => opt.MapFrom(src => src.ParticipantType.ToString()));

            CreateMap<TourBookingForm, GetBookingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                 .ForMember(dest => dest.IsEditAllowed, opt => opt.MapFrom(src => src.IsEditAllowed))
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.ParticipantInformations))
                .ForMember(dest => dest.ParticipantType, opt => opt.MapFrom(src => src.ParticipantType.ToString()));

            // ✅ Add PATCH mapping here
            CreateMap<PatchTourBookingDto, TourBookingForm>()
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => src.Status != null))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<BookStatus>(src.Status, true)))

                //.ForMember(dest => dest.ParticipantType,
                //    opt => opt.Condition(src => src.ParticipantType != null))
                //.ForMember(dest => dest.ParticipantType,
                //    opt => opt.MapFrom(src => Enum.Parse<ParticipantType>(src.ParticipantType, true)))

                .ForAllMembers(opt => opt.Condition((src, dest, val) => val != null));

            //✅ NEW IMPORTANT MAPPING
        CreateMap<ParticipantInformation, ParticipantDto>();

            // ✅ If not already mapped in UserProfile
            CreateMap<AuthUser, UserResponseDto>();
        }
    }

}

