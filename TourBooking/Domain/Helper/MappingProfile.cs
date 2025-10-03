using AutoMapper;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Participant.DTO;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourNote.DTO;
using Domain.Services.Terms.DTO;
using Domain.Services.User.DTO;
using Domain.Services.Tour.DTO;
using Domain.Enums;

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

            CreateMap<Tourss, TourDto>()
           .ForMember(dest => dest.DestinationName,
                      opt => opt.MapFrom(src => src.Destination.Name)) // ✅ flatten Destination.Name
           .ForMember(dest=>dest.ImageFile,opt=>opt.MapFrom(src=>src.Destination.ImageUrl))
           .ForMember(dest => dest.Status,
                      opt => opt.MapFrom(src => src.Status.ToString()));// enum → string
           

            CreateMap<TourDto,Tourss>().ReverseMap();

            CreateMap<AddUserDto, AuthUser>().ReverseMap();

            CreateMap<AuthUser, UserResponseDto>().ReverseMap();

        }

        }
    }

