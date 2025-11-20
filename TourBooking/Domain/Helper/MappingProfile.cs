using AutoMapper;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Participant.DTO;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourNote.DTO;
using Domain.Services.Terms.DTO;
using Domain.Services.Users.DTO;
using Domain.Services.Tour.DTO;
using Domain.Enums;

namespace Domain.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
           
            CreateMap<Destination, DestinationDto>().ReverseMap();
            CreateMap<Destination, DestinationResponseDto>().ReverseMap();

            CreateMap<TermsDto, TermsAndCondition>().ReverseMap();

        }

        }
    }

