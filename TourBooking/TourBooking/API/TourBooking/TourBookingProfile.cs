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
            // ---------------------------------------------------------------
            // BASIC REQUEST ↔ DTO MAPPINGS
            // ---------------------------------------------------------------
            CreateMap<AddTourBookingRequest, TourBookingDto>();
            CreateMap<TourBookingDto, AddTourBookingRequest>();


            // ---------------------------------------------------------------
            // DTO → FORM (Enum string → Enum)
            // ---------------------------------------------------------------
            CreateMap<TourBookingDto, TourBookingForm>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<BookStatus>(src.Status, true)))
                .ForMember(dest => dest.EditStatusCheck,
                    opt => opt.MapFrom(src => Enum.Parse<EditStatus>(src.EditStatusCheck, true)))
                .ForMember(dest => dest.ParticipantType,
                    opt => opt.MapFrom(src => Enum.Parse<ParticipantType>(src.ParticipantType, true)));


            // ---------------------------------------------------------------
            // FORM → DTO (Enum → string)
            // ---------------------------------------------------------------
            CreateMap<TourBookingForm, TourBookingDto>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.EditStatusCheck,
                    opt => opt.MapFrom(src => src.EditStatusCheck.ToString()))
                .ForMember(dest => dest.ParticipantType,
                    opt => opt.MapFrom(src => src.ParticipantType.ToString()))
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User));


            // ---------------------------------------------------------------
            // FORM → GET DTO (Used for API output)
            // ---------------------------------------------------------------
            CreateMap<TourBookingForm, GetBookingDto>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.EditStatusCheck,
                    opt => opt.MapFrom(src => src.EditStatusCheck.ToString()))
                .ForMember(dest => dest.User,
                    opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.IsEditAllowed,
                    opt => opt.MapFrom(src => src.IsEditAllowed))
                .ForMember(dest => dest.Participants,
                    opt => opt.MapFrom(src => src.ParticipantInformations))
                .ForMember(dest => dest.ParticipantType,
                    opt => opt.MapFrom(src => src.ParticipantType.ToString()));


            // ---------------------------------------------------------------
            // PATCH → FORM (Only update non-null fields)
            // ---------------------------------------------------------------
            CreateMap<PatchTourBookingDto, TourBookingForm>()
                // Status
                .ForMember(dest => dest.Status,
                    opt => opt.Condition(src => src.Status != null))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => Enum.Parse<BookStatus>(src.Status!, true)))

                // Only apply when value != null
                .ForAllMembers(opt => opt.Condition((src, dest, val) => val != null));

            // These are needed for internal service flow
            CreateMap<TourBookingDto, TourBookingForm>();
            CreateMap<TourBookingForm, TourBookingDto>();


            // ---------------------------------------------------------------
            // UPDATE (PUT) → FORM
            // ---------------------------------------------------------------
            CreateMap<UpdateTourBookingDto, TourBookingForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }


}

