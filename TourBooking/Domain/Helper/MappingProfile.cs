using AutoMapper;
using Domain.Models;
using Domain.Services.Participant.DTO;
using Domain.Services.TourBooking.DTO;
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

            //CreateMap<TourBookingDto, TourBookingForm>();

            ////If you also need reverse mapping(Entity -> DTO)
            //CreateMap<TourBookingForm, TourBookingDto>();

            ////CreateMap<UpdateTourBookingDto, TourBookingForm>();

            //// Entity -> Read DTO (for returning to client)
            ////CreateMap<TourBookingForm, TourBookingDto>();
            //CreateMap<UpdateTourBookingDto, TourBookingForm>()
            //.ForAllMembers(opts => opts.Condition(
            //    (src, dest, srcMember) => srcMember != null
            //));
            //CreateMap<UpdateTourBookingDto, PartialTourBookingDto>();
            //CreateMap<PartialTourBookingDto, UpdateTourBookingDto>();
            // TourBooking mappings
            CreateMap<TourBookingDto, TourBookingForm>();
            CreateMap<TourBookingForm, TourBookingDto>();

            CreateMap<UpdateTourBookingDto, TourBookingForm>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ✅ Add this
            CreateMap<TourBookingForm, PartialTourBookingDto>();

            // Optional DTO ↔ DTO if needed
            CreateMap<UpdateTourBookingDto, PartialTourBookingDto>();
            CreateMap<PartialTourBookingDto, UpdateTourBookingDto>();

        }
    }
}
