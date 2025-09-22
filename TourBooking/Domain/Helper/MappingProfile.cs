using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Participant.DTO;
using Domain.Services.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Services.TourBooking;
using System.Threading.Tasks; 

namespace Domain.Helper
{
    public class MappingProfile:Profile
    { 
             public MappingProfile()
            {
                // Entity → DTO
                CreateMap<ParticipantInformation, ParticipantDto>()
                    .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.LeadId));

<<<<<<< HEAD
            // DTO → Entity
            CreateMap<ParticipantDto, ParticipantInformation>()
                .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.BookingId));

            CreateMap<Destination, DestinationDto>().ReverseMap();
            CreateMap<Destination, DestinationResponseDto>().ReverseMap();


=======
                // DTO → Entity
                CreateMap<ParticipantDto, ParticipantInformation>()
                    .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.BookingId));

            CreateMap<AddUserDto, AuthUser>() .ReverseMap(); 

            CreateMap<AuthUser, UserResponseDto>().ReverseMap(); ;
        }
>>>>>>> df226a5ffc4e5b1e939f88eeb15eb131fdeb9629
        }
    }

