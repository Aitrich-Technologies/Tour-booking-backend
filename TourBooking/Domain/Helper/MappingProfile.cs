using AutoMapper;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Participant.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            // DTO → Entity
            CreateMap<ParticipantDto, ParticipantInformation>()
                .ForMember(dest => dest.LeadId, opt => opt.MapFrom(src => src.BookingId));

            CreateMap<Destination, DestinationDto>().ReverseMap();
            CreateMap<Destination, DestinationResponseDto>().ReverseMap();


        }
    }
}
