using AutoMapper;
using Domain.Models;
using Domain.Services.Participant.DTO;
using Domain.Services.TourBooking.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.TourBooking.DTO;
using Domain.Models;

using Domain.Services.TourBooking.DTO;



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
            }
        }
    }

