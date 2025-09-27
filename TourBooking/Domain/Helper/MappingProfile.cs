using AutoMapper;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Participant.DTO;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourNote.DTO;
using Domain.Services.Terms.DTO;
using Domain.Services.User.DTO;
using Domain.Services.Tour.DTO;

namespace Domain.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Entity → DTO
            CreateMap<ParticipantInformation, ParticipantDto>()
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.LeadId));

            // DTO → Entity
            CreateMap<ParticipantDto, ParticipantInformation>()
                .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.BookingId));
                                    
             CreateMap<TourBookingDto, TourBookingForm>();
             CreateMap<TourBookingForm, TourBookingDto>();      
            
            CreateMap<UpdateTourBookingDto, TourBookingForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
           


            // DTO → Entity
            CreateMap<ParticipantDto, ParticipantInformation>()
                .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.BookingId));

            CreateMap<Destination, DestinationDto>().ReverseMap();
            CreateMap<Destination, DestinationResponseDto>().ReverseMap();



            // DTO → Entity
            CreateMap<ParticipantDto, ParticipantInformation>()
                .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.BookingId));

            CreateMap<Notes, NoteDto>().ReverseMap();


            CreateMap<TermsDto, TermsAndCondition>().ReverseMap();
            CreateMap<TourDto,Tourss>().ReverseMap();

            CreateMap<AddUserDto, AuthUser>().ReverseMap();

            CreateMap<AuthUser, UserResponseDto>().ReverseMap();

        }

        }
    }

